using System.Text.Json.Serialization;

namespace AzureAI.Speech.Types
{
    public class CollectionResponse<T>
    {
        [JsonPropertyName("nextLink")]
        public string? NextLink { get; set; }
        [JsonPropertyName("value")]
        public IEnumerable<T>? Value { get; set; }
    }
}
