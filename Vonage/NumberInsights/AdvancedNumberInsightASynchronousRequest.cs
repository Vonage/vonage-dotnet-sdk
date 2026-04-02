using Newtonsoft.Json;

namespace Vonage.NumberInsights;

/// <summary>
///     Represents a request to perform an asynchronous advanced Number Insight lookup.
///     Results are delivered to the specified callback URL.
/// </summary>
public class AdvancedNumberInsightAsynchronousRequest : AdvancedNumberInsightRequest
{
    /// <summary>
    ///     The webhook URL where the advanced insight results will be sent.
    ///     Must be a publicly accessible HTTPS endpoint.
    /// </summary>
    [JsonProperty("callback")]
    public string Callback { get; set; }
}