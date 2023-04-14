using Newtonsoft.Json;

namespace Vonage.Redaction;

public class RedactRequest
{
    /// <summary>
    /// The transaction ID to redact
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Product name that the ID provided relates to
    /// </summary>
    [JsonProperty("product")]
    public RedactionProduct? Product { get; set; }

    /// <summary>
    /// Required if redacting SMS data
    /// </summary>
    [JsonProperty("type")]
    public RedactionType? Type { get; set; }
}