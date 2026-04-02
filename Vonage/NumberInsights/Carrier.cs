using Newtonsoft.Json;

namespace Vonage.NumberInsights;

/// <summary>
///     Represents carrier (mobile network operator) information for a phone number.
/// </summary>
public class Carrier
{
    /// <summary>
    ///     The Mobile Country Code (MCC) and Mobile Network Code (MNC) for the carrier.
    ///     Invalid numbers are marked as "unknown" and impossible E.164 numbers are rejected.
    /// </summary>
    [JsonProperty("network_code")]
    public string NetworkCode { get; set; }

    /// <summary>
    ///     The full name of the carrier the number is associated with (e.g., "Vodafone", "AT&amp;T").
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     The country where the carrier operates, in ISO 3166-1 alpha-2 format (e.g., "GB", "US").
    /// </summary>
    [JsonProperty("country")]
    public string Country { get; set; }

    /// <summary>
    ///     The type of network: mobile, landline, virtual, premium, or toll-free.
    /// </summary>
    [JsonProperty("network_type")]
    public string NetworkType { get; set; }
}