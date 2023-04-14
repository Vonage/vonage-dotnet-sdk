using Newtonsoft.Json;

namespace Vonage.NumberInsights;

public class AdvancedNumberInsightAsynchronousRequest : AdvancedNumberInsightRequest
{
    /// <summary>
    /// The callback URL
    /// </summary>
    [JsonProperty("callback")]
    public string Callback { get; set; }
}