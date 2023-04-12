using Newtonsoft.Json;

namespace Vonage.NumberInsights;

public class NumberInsightResponseBase
{
    /// <summary>
    /// Status of the Number Insight Request
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// The unique identifier for your request. This is a alphanumeric string up to 40 characters.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }
}