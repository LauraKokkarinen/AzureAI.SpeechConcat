using AzureAI.Speech;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using Microsoft.Extensions.Configuration;

class Program
{
    async static Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        string? speechKey = configuration["SpeechKey"];
        string? speechRegion = configuration["SpeechRegion"];
        string? textFilePath = configuration["TextFilePath"];
        string? voiceName = configuration["VoiceName"];
        string? language = configuration["Language"];
        string? temperature = configuration["Temperature"];
        string? voiceStyle = configuration["VoiceStyle"];
        string? styleDegree = configuration["StyleDegree"];
        string? audioFileName = configuration["AudioFileName"];
        _ = bool.TryParse(configuration["UseBatchSynth"], out bool useBatchSynth);
        _ = bool.TryParse(configuration["SaveSsml"], out bool saveSsml);

        if (string.IsNullOrEmpty(speechKey) || string.IsNullOrEmpty(speechRegion) || string.IsNullOrEmpty(textFilePath))
            throw new Exception("Missing required configuration values.");

        var directoryPath = Path.GetDirectoryName(textFilePath) ?? throw new Exception("Text file path is invalid.");
        var batches = PrepareBatches(textFilePath, 5000, voiceName, language, temperature, voiceStyle, styleDegree, saveSsml);

        var audioFilePaths = useBatchSynth ?
            await ConcatBatchSynthesizer.Run(speechKey, speechRegion, batches, directoryPath) :
            await ConcatSpeechSynthesizer.Run(speechKey, speechRegion, batches, directoryPath);

        ConcatAudioFiles(audioFilePaths, directoryPath, $"{audioFileName ?? "result"}.wav");
    }    

    private static List<string> PrepareBatches(string textFilePath, int batchLength, string? voiceName, string? language, string? temperature, string? voiceStyle, string? styleDegree, bool? saveSsml)
    {
        var batches = new List<string>();

        var textContent = File.ReadAllText(textFilePath);
        var ssmlContent = $"<p>{textContent.Replace("\r\n", "</p><break strength=\"strong\"/><p>").Replace("***", "<break strength=\"x-strong\"/>")}</p><break strength=\"strong\"/>";

        var startIndex = 0;
        while (startIndex < ssmlContent.Length)
        {
            var endIndex = startIndex + batchLength < ssmlContent.Length ? ssmlContent.LastIndexOf("/>", startIndex + batchLength) + 2 : ssmlContent.Length;
            var content = ssmlContent.Substring(startIndex, endIndex - startIndex);

            var batch = $"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:mstts=\"https://www.w3.org/2001/mstts\" xml:lang=\"{language}\"><voice name=\"{voiceName ?? "en-US-Ava:DragonHDLatestNeural"}\" parameters=\"temperature={temperature ?? "1.0"}\"><mstts:express-as style=\"{voiceStyle}\" styledegree=\"{styleDegree}\">{content}</mstts:express-as></voice></speak>";
            batches.Add(batch);

            startIndex = endIndex;

            if (saveSsml == true)
                File.WriteAllText($"{Path.GetDirectoryName(textFilePath)}\\{startIndex}.ssml", string.Join("", batch));
        }

        return batches;
    }    

    private static void ConcatAudioFiles(IEnumerable<string> inputFilePaths, string directoryPath, string outputFileName)
    {
        var audioFiles = new List<AudioFileReader>();

        foreach (var inputFilePath in inputFilePaths)
        {
            audioFiles.Add(new AudioFileReader(inputFilePath));
        }

        var concatResult = new ConcatenatingSampleProvider(audioFiles);

        WaveFileWriter.CreateWaveFile16($"{directoryPath}\\{outputFileName}", concatResult);

        foreach (var audioFile in audioFiles)
        {
            audioFile.Close();
            audioFile.Dispose();

            var directory = Path.GetDirectoryName(audioFile.FileName);
            if (directory != null && directory != directoryPath)
            {
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                    File.Delete(file);

                Directory.Delete(directory);
            }
            else
                File.Delete(audioFile.FileName);
        }
    }
}
