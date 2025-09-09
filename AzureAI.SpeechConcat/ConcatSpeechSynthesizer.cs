using Microsoft.CognitiveServices.Speech;

namespace AzureAI.Speech
{
    internal static class ConcatSpeechSynthesizer
    {
        internal static async Task<IEnumerable<string>> Run(string speechKey, string speechRegion, string directoryPath, List<string> batches)
        {
            var audioFilePaths = new List<string>();

            foreach (var batch in batches)
            {
                audioFilePaths.Add(await UseSpeechSynthesizer(speechKey, speechRegion, directoryPath, batch));
            }
            
            return audioFilePaths;
        }

        private static async Task<string> UseSpeechSynthesizer(string speechKey, string speechRegion, string directoryPath, string content)
        {
            var audioFilePath = $"{directoryPath}\\{Guid.NewGuid()}.wav";

            // Configure the speech synthesizer
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Perform the conversion
                var result = await speechSynthesizer.SpeakSsmlAsync(content);

                // Save the result into an audio file
                using var stream = AudioDataStream.FromResult(result);
                await stream.SaveToWaveFileAsync(audioFilePath);
            }

            return audioFilePath;
        }
    }
}
