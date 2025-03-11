using AzureAI.Speech;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using Microsoft.Extensions.Configuration;
using AzureAI.Speech.Helpers;

class Program
{
    async static Task Main(string[] args)
    {
        var appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var configuration = new ConfigurationHelper(appSettings);

        string speechKey = configuration.GetConfigurationValue<string>("SpeechKey", true)!;
        string speechRegion = configuration.GetConfigurationValue<string>("SpeechRegion", true)!;
        string textFilePath = configuration.GetConfigurationValue<string>("TextFilePath", true)!;
        string? voiceName = configuration.GetConfigurationValue<string?>("VoiceName");
        string? language = configuration.GetConfigurationValue<string?>("Language");
        string? temperature = configuration.GetConfigurationValue<string?>("Temperature");
        string? speedIncrease = configuration.GetConfigurationValue<string?>("SpeedIncrease");
        string? voiceStyle = configuration.GetConfigurationValue<string?>("SpeakingStyle");
        string? styleDegree = configuration.GetConfigurationValue<string?>("StyleDegree");
        string? audioFileName = configuration.GetConfigurationValue<string?>("AudioFileName");
        int? breakDuration = configuration.GetConfigurationValue<int?>("BreakDuration");
        bool? useBatchSynth = configuration.GetConfigurationValue<bool?>("UseBatchSynth");
        bool? saveSsml = configuration.GetConfigurationValue<bool?>("SaveSsml");

        var directoryPath = Path.GetDirectoryName(textFilePath) ?? throw new Exception("Text file path is invalid.");
        var batches = PrepareBatches(textFilePath, 5000, voiceName, language, temperature, speedIncrease, voiceStyle, styleDegree, breakDuration, saveSsml);

        var audioFilePaths = useBatchSynth == true ?
            await ConcatBatchSynthesizer.Run(speechKey, speechRegion, batches, directoryPath) :
            await ConcatSpeechSynthesizer.Run(speechKey, speechRegion, batches, directoryPath);

        ConcatAudioFiles(audioFilePaths, directoryPath, $"{audioFileName ?? "result"}.wav");
    }    

    private static List<string> PrepareBatches(string textFilePath, int batchLength, string? voiceName, string? language, string? temperature, string? speedIncrease, string? voiceStyle, string? styleDegree, int? breakDuration, bool? saveSsml)
    {
        var batches = new List<string>();

        var textContent = File.ReadAllText(textFilePath);
        var breakTag = $"<break time=\"{breakDuration ?? 500}ms\"/>";
        var ssmlContent = $"<p>{textContent.Replace("\r\n", "</p><p>").Replace("<p></p>", "").Replace("<p/>", "").Replace("</p><p>", $"</p>{breakTag}<p>").Replace("***", $"{breakTag}{breakTag}")}</p>{breakTag}";

        var startIndex = 0;
        while (startIndex < ssmlContent.Length)
        {
            var endIndex = startIndex + batchLength < ssmlContent.Length ? ssmlContent.LastIndexOf(breakTag, startIndex + batchLength) + breakTag.Length : ssmlContent.Length;
            var content = ssmlContent.Substring(startIndex, endIndex - startIndex);

            var batch = $"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:mstts=\"https://www.w3.org/2001/mstts\" xml:lang=\"{language ?? "en-US"}\"><voice name=\"{voiceName ?? "en-US-Ava:DragonHDLatestNeural"}\" parameters=\"temperature={temperature ?? "1.0"}\"><prosody rate=\"{speedIncrease ?? "0"}%\" pitch=\"0%\"><mstts:express-as style=\"{voiceStyle ?? "story"}\" styledegree=\"{styleDegree ?? "2"}\">{content}</mstts:express-as></prosody></voice></speak>";
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
