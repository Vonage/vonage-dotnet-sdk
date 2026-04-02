using Newtonsoft.Json;

namespace Vonage.NumberInsights;

/// <summary>
///     Represents a request to perform a standard Number Insight lookup on a phone number,
///     including carrier and porting information.
/// </summary>
public class StandardNumberInsightRequest : BasicNumberInsightRequest
{
    /// <summary>
    /// Indicates if the name of the person who owns the phone number should be looked up and returned in the response. 
    /// Set to true to receive phone number owner name in the response. 
    /// This features is available for US numbers only and incurs an additional charge.
    /// </summary>
    [JsonProperty("cnam")]
    public bool? Cnam { get; set; }
}