using System.Text.Json.Serialization;

namespace AzureAI.Speech.Types
{
    public class BatchSynthesis
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("internalId")]
        public string? InternalId { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }
        [JsonPropertyName("lastActionDateTime")]
        public DateTime LastActionDateTime { get; set; }
        [JsonPropertyName("inputKind")]
        public string? InputKind { get; set; }
        [JsonPropertyName("customVoices")]
        public object? CustomVoices { get; set; }
        [JsonPropertyName("properties")]
        public Properties? Properties { get; set; }
        [JsonPropertyName("outputs")]
        public Outputs? Outputs { get; set; }
    }

    public class Properties
    {
        [JsonPropertyName("timeToLiveInHours")]
        public int TimeToLiveInHours { get; set; }
        [JsonPropertyName("outputFormat")]
        public string? OutputFormat { get; set; }
        [JsonPropertyName("concatenateResult")]
        public bool ConcatenateResult { get; set; }
        [JsonPropertyName("decompressOutputFiles")]
        public bool DecompressOutputFiles { get; set; }
        [JsonPropertyName("wordBoundaryEnabled")]
        public bool WordBoundaryEnabled { get; set; }
        [JsonPropertyName("sentenceBoundaryEnabled")]
        public bool SentenceBoundaryEnabled { get; set; }
        [JsonPropertyName("sizeInBytes")]
        public int SizeInBytes { get; set; }
        [JsonPropertyName("succeededAudioCount")]
        public int SucceededAudioCount { get; set; }
        [JsonPropertyName("failedAudioCount")]
        public int FailedAudioCount { get; set; }
        [JsonPropertyName("durationInMilliseconds")]
        public int DurationInMilliseconds { get; set; }
        [JsonPropertyName("billingDetails")]
        public BillingDetails? BillingDetails { get; set; }
    }

    public class BillingDetails
    {
        [JsonPropertyName("neuralCharacters")]
        public int NeuralCharacters { get; set; }
    }

    public class Outputs
    {
        [JsonPropertyName("result")]
        public string? Result { get; set; }
    }
}
