using Newtonsoft.Json;

namespace Vonage.Pricing;

/// <summary>
///     Represents a request to retrieve pricing information for countries matching a dialing prefix.
/// </summary>
public class PricingPrefixRequest
{
    /// <summary>
    /// The numerical dialing prefix to look up pricing for. Examples include 44, 1 and so on.
    /// </summary>
    [JsonProperty("prefix")]
    public string Prefix { get; set; }
}