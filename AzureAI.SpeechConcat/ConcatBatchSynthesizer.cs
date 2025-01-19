using AzureAI.Speech.Enums;
using AzureAI.Speech.Services;
using AzureAI.Speech.Types;
using System.IO.Compression;

namespace AzureAI.Speech
{
    internal static class ConcatBatchSynthesizer
    {
        internal static async Task<List<string>> Run(string speechKey, string speechRegion, List<string> batches, string directoryPath)
        {
            var speechService = new SpeechService(new HttpService(), speechRegion, speechKey);

            var synthesisIds = await CreateBatchSyntheses(speechService, batches);
            await WaitForSynthesesToComplete(speechService, synthesisIds);
            var zipFilePaths = await DownloadSynthesisResults(speechService, synthesisIds, directoryPath);
            var audioFilePaths = ExtractAudioFiles(zipFilePaths);

            return audioFilePaths;
        }

        private static async Task<IEnumerable<string>> CreateBatchSyntheses(SpeechService speechService, List<string> batches)
        {
            var synthesisIds = new List<string>();
            foreach (var batch in batches)
            {
                var synthesis = await speechService.CreateBatchSynthesis(batch);
                if (synthesis?.Id != null)
                    synthesisIds.Add(synthesis.Id);
            }

            return synthesisIds;
        }

        private static async Task WaitForSynthesesToComplete(SpeechService speechService, IEnumerable<string> synthesisIds)
        {
            var inProgress = true;
            while (inProgress)
            {
                await Task.Delay(5000);

                var batchSyntheses = new List<BatchSynthesis>();
                foreach (var synthesisId in synthesisIds)
                {
                    var synthesis = await speechService.GetBatchSynthesis(synthesisId);
                    if (synthesis != null)
                        batchSyntheses.Add(synthesis);
                }

                inProgress = batchSyntheses.Any(s => s.Status == SynthesisStatus.Running.ToString() || s.Status == SynthesisStatus.NotStarted.ToString());
            }
        }

        private static async Task<IEnumerable<string>> DownloadSynthesisResults(SpeechService speechService, IEnumerable<string> synthesisIds, string? directoryPath)
        {
            var zipFilePaths = new List<string>();

            using var httpClient = new HttpClient();
            foreach (var synthesisId in synthesisIds)
            {
                var zipFilePath = $"{directoryPath}\\{synthesisId}.zip";
                var synthesis = await speechService.GetBatchSynthesis(synthesisId);

                using var stream = await httpClient.GetStreamAsync(synthesis?.Outputs?.Result);
                using var fileStream = new FileStream(zipFilePath, FileMode.OpenOrCreate);
                await stream.CopyToAsync(fileStream);

                zipFilePaths.Add(zipFilePath);
                await speechService.DeleteBatchSynthesis(synthesisId);
            }

            return zipFilePaths;
        }

        private static List<string> ExtractAudioFiles(IEnumerable<string> zipFilePaths)
        {
            var audioFilePaths = new List<string>();

            foreach (var zipFilePath in zipFilePaths)
            {
                var extractDirName = Path.GetFileNameWithoutExtension(zipFilePath).Replace(".zip", "");
                var extractDirPath = $"{Path.GetDirectoryName(zipFilePath)}\\{extractDirName}";

                ZipFile.ExtractToDirectory(zipFilePath, extractDirPath);
                var extractDirInfo = new DirectoryInfo(extractDirPath);

                var audioFiles = extractDirInfo.GetFiles("*.wav");
                foreach (var audioFile in audioFiles)
                {
                    audioFilePaths.Add(audioFile.FullName);
                }

                File.Delete(zipFilePath);
            }

            return audioFilePaths;
        }
    }
}
