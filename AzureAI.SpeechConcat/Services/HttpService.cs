using AzureAI.Speech.Enums;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AzureAI.Speech.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<JsonElement?> GetResponseAsync(string url, Method method, HttpRequestHeaders? headers = null, string? body = null, string? contentType = null)
        {
            var request = new HttpRequestMessage(new HttpMethod(method.ToString()), url);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            request.Content = body != null ? new StringContent(body, Encoding.UTF8) : null;

            if (request.Content != null)
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType ?? "application/json");

            var response = _httpClient.SendAsync(request).Result;

            if (response != null)
            {
                var status = (int)response.StatusCode;
                bool throttled = status == 429;

                if (throttled || status == 502 || status == 504)
                {
                    if (throttled)
                    {
                        var timeSpan = response.Headers.RetryAfter?.Delta?.Seconds;
                        int milliseconds = (timeSpan ?? 5) * 1000;
                        Thread.Sleep(milliseconds);
                    }

                    return await GetResponseAsync(url, method, headers, body, contentType); //retry
                }
            }

            var responseBody = await ReadResponseBody(response);

            if (response?.IsSuccessStatusCode == true)
            {
                if (response?.StatusCode == HttpStatusCode.Accepted && response.Headers.Location != null)
                    responseBody = GetResponseHeaders(response);

                return responseBody;
            }
            else
            {
                if (response?.StatusCode == HttpStatusCode.Conflict && response.Headers.Location != null)
                    return GetResponseHeaders(response);

                throw new Exception(responseBody?.ToString() ?? response?.ReasonPhrase);
            }
        }

        private static async Task<JsonElement?> ReadResponseBody(HttpResponseMessage? response)
        {
            if (response != null && response?.Content != null)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<JsonElement>(content);
                }
                catch (Exception) // Response content is not in JSON format
                {
                    return null;
                }
            }

            return null;
        }

        private static JsonElement GetResponseHeaders(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(response.Headers));
        }
    }
}
