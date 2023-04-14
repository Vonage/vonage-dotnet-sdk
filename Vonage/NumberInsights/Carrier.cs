using Newtonsoft.Json;

namespace Vonage.NumberInsights;

public class Carrier
{
    /// <summary>
    /// The https://en.wikipedia.org/wiki/Mobile_country_code for the carriernumber is associated with.
    /// Unreal numbers are marked asunknown and the request is rejected altogether if the number is impossible according to the E.164 guidelines.
    /// </summary>
    [JsonProperty("network_code")]
    public string NetworkCode { get; set; }

    /// <summary>
    /// The full name of the carrier that number is associated with.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// The country that number is associated with. This is in ISO 3166-1 alpha-2 format.
    /// </summary>
    [JsonProperty("country")]
    public string Country { get; set; }
    /// <summary>
    /// The type of network that number is associated with.
    /// </summary>
    [JsonProperty("network_type")]
    public string NetworkType { get; set; }
}