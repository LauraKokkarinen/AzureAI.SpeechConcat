using AzureAI.Speech.Enums;
using AzureAI.Speech.Types;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AzureAI.Speech.Services
{
    public class SpeechService(HttpService httpService, string region, string key)
    {
        private readonly HttpService _httpService = httpService;
        private readonly string _region = region;
        private readonly string _key = key;

        /// <summary>
        /// Get the headers for a request.
        /// </summary>
        /// <returns>Ocp-Apim-Subscription-Key header with the speech key.</returns>
        private HttpRequestHeaders GetHeaders()
        {
            var headers = new HttpRequestMessage().Headers;

            headers.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", _key);

            return headers;
        }

        /// <summary>
        /// Get a single item.
        /// </summary>
        /// <typeparam name="T">The type of object you are expecting to receive in the response</typeparam>
        /// <param name="url">URL of the request</param>
        /// <returns>A single item deserialized into the provided object type.</returns>
        private async Task<T?> Get<T>(string url)
        {
            var response = await _httpService.GetResponseAsync(url, Method.Get, GetHeaders());

            return response.Value.Deserialize<T>();
        }

        /// <summary>
        /// Get a collection of items (with paging).
        /// </summary>
        /// <typeparam name="T">The type of object you are expecting to receive in the response collection</typeparam>
        /// <param name="url">URL of the request</param>
        /// <param name="items">When recursively paging through the retrieved items, this parameter contains the items collected so far.</param>
        /// <returns>A collection of items deserialized into the provided object type.</returns>
        private async Task<IEnumerable<T>> GetCollection<T>(string url, IEnumerable<T>? items = null)
        {
            items ??= [];

            var response = await Get<CollectionResponse<T>>(url);

            if (response != null)
            {
                if (response?.Value != null)
                    items = items.Concat(response.Value);

                if (response?.NextLink != null)
                    return await GetCollection<T>(response.NextLink, items);
            }

            return items;
        }

        /// <summary>
        /// Make a put request.
        /// </summary>
        /// <typeparam name="T">The type of object you are expecting to receive in the response</typeparam>
        /// <param name="url">URL of the request</param>
        /// <param name="body">Optional body of the request</param>
        /// <returns>The added object.</returns>
        private async Task<T?> Put<T>(string url, string body)
        {
            var response = await _httpService.GetResponseAsync(url, Method.Put, GetHeaders(), body);

            return response.Value.Deserialize<T>();
        }

        /// <summary>
        /// Make a delete request.
        /// </summary>
        /// <param name="url">URL of the request</param>
        private async Task Delete(string url)
        {
            await _httpService.GetResponseAsync(url, Method.Delete, GetHeaders());
        }

        /// <summary>
        /// Create a batch synthesis.
        /// </summary>
        /// <param name="content">SSML XML content</param>
        /// <param name="description">Optional description for the synthesis</param>
        /// <returns>A batch synthesis object.</returns>
        public async Task<BatchSynthesis?> CreateBatchSynthesis(string content, string? description = null)
        {
            var uniqueId = Guid.NewGuid();
            var body = new
            {
                description,
                inputKind = "SSML",
                inputs = new[]
                {
                    new
                    {
                        content
                    }
                },
                properties = new
                {
                    outputFormat = "riff-48khz-16bit-mono-pcm",
                    wordBoundaryEnabled = false,
                    sentenceBoundaryEnabled = false,
                    concatenateResult = false,
                    decompressOutputFiles = false
                }
            };

            return await Put<BatchSynthesis>($"https://{_region}.api.cognitive.microsoft.com/texttospeech/batchsyntheses/{uniqueId}?api-version=2024-04-01", JsonSerializer.Serialize(body));
        }

        /// <summary>
        /// Get the batch synthesis details.
        /// </summary>
        /// <param name="id">The batch synthesis ID</param>
        /// <returns>The batch synthesis details.</returns>
        public async Task<BatchSynthesis?> GetBatchSynthesis(string id)
        {
            return await Get<BatchSynthesis>($"https://{_region}.api.cognitive.microsoft.com/texttospeech/batchsyntheses/{id}?api-version=2024-04-01");
        }

        /// <summary>
        /// Delete the batch synthesis.
        /// </summary>
        /// <param name="id">The batch synthesis ID</param>
        /// <returns></returns>
        public async Task DeleteBatchSynthesis(string id)
        {
            await Delete($"https://{_region}.api.cognitive.microsoft.com/texttospeech/batchsyntheses/{id}?api-version=2024-04-01");
        }
    }
}
