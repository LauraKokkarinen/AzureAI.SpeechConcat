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
        string? temperature = configuration["Temperature"];        
        string? audioFileName = configuration["AudioFileName"];
        _ = bool.TryParse(configuration["UseBatchSynth"], out bool useBatchSynth);

        if (string.IsNullOrEmpty(speechKey) || string.IsNullOrEmpty(speechRegion) || string.IsNullOrEmpty(textFilePath))
            throw new Exception("Missing required configuration values.");

        var directoryPath = Path.GetDirectoryName(textFilePath) ?? throw new Exception("Text file path is invalid.");
        var batches = PrepareBatches(textFilePath, 5000, voiceName, temperature);

        var audioFilePaths = useBatchSynth ?
            await ConcatBatchSynthesizer.Run(speechKey, speechRegion, batches, directoryPath) :
            await ConcatSpeechSynthesizer.Run(speechKey, speechRegion, batches, directoryPath);

        ConcatAudioFiles(audioFilePaths, $"{directoryPath}\\{audioFileName ?? "result"}.wav");
    }    

    private static List<string> PrepareBatches(string textFilePath, int batchLength, string? voiceName, string? temperature)
    {
        var batches = new List<string>();

        var textContent = File.ReadAllText(textFilePath);
        var ssmlContent = $"<p>{textContent.Replace("\r\n", "</p><break strength=\"strong\"/><p>").Replace("***", "<break strength=\"x-strong\"/>")}</p><break strength=\"strong\"/>";

        var startIndex = 0;
        while (startIndex < ssmlContent.Length)
        {
            var endIndex = startIndex + batchLength < ssmlContent.Length ? ssmlContent.LastIndexOf("/>", startIndex + batchLength) + 2 : ssmlContent.Length;
            var batch = ssmlContent.Substring(startIndex, endIndex - startIndex);

            batches.Add($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\"><voice name=\"{voiceName ?? "en-US-Ava:DragonHDLatestNeural"}\" parameters=\"temperature={temperature ?? "1.0"}\">{batch}</voice></speak>");

            startIndex = endIndex;
        }

        return batches;
    }    

    private static void ConcatAudioFiles(IEnumerable<string> inputFilePaths, string outputFilePath)
    {
        var audioFiles = new List<AudioFileReader>();

        foreach (var inputFilePath in inputFilePaths)
        {
            audioFiles.Add(new AudioFileReader(inputFilePath));
        }

        var concatResult = new ConcatenatingSampleProvider(audioFiles);

        WaveFileWriter.CreateWaveFile16(outputFilePath, concatResult);

        foreach (var audioFile in audioFiles)
        {
            audioFile.Close();
            audioFile.Dispose();

            var directory = Path.GetDirectoryName(audioFile.FileName);
            if (directory != null)
            {
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                    File.Delete(file);

                Directory.Delete(directory);
            }
        }
    }
}
