using Microsoft.CognitiveServices.Speech;

namespace AzureAI.Speech
{
    internal static class ConcatSpeechSynthesizer
    {
        internal static async Task<IEnumerable<string>> Run(string speechKey, string speechRegion, List<string> batches, string directoryPath)
        {
            var audioFilePaths = new List<string>();

            foreach (var batch in batches)
            {
                audioFilePaths.Add(await UseSpeechSynthesizer(speechKey, speechRegion, batch, directoryPath));
            }
            
            return audioFilePaths;
        }

        private static async Task<string> UseSpeechSynthesizer(string speechKey, string speechRegion, string content, string? directoryPath)
        {
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            var audioFilePath = $"{directoryPath}\\{Guid.NewGuid()}.wav";

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                var result = await speechSynthesizer.SpeakSsmlAsync(content);

                using var stream = AudioDataStream.FromResult(result);
                await stream.SaveToWaveFileAsync(audioFilePath);
            }

            return audioFilePath;
        }
    }
}
