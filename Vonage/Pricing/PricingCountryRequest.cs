using Newtonsoft.Json;

namespace Vonage.Pricing;

/// <summary>
///     Represents a request to retrieve pricing information for a specific country.
/// </summary>
public class PricingCountryRequest
{
    /// <summary>
    /// A two letter country code. For example, CA.
    /// </summary>
    [JsonProperty("country")]
    public string Country { get; set; }
}