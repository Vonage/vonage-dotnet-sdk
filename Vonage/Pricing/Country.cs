using Newtonsoft.Json;

namespace Vonage.Pricing;

public class Country
{
    /// <summary>
    /// Two letter country code.
    /// </summary>
    [JsonProperty("countryCode")]
    public string CountryCode { get; set; }

    /// <summary>
    /// Readable country name.
    /// </summary>
    [JsonProperty("countryName")]
    public string CountryName { get; set; }

    /// <summary>
    /// Readable country name.
    /// </summary>
    [JsonProperty("countryDisplayName")]
    public string CountryDisplayName { get; set; }

    /// <summary>
    /// The currency that your account is being billed in (by default Euros—EUR). 
    /// Can change in the Dashboard to US Dollars—USD.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// The default price.
    /// </summary>
    [JsonProperty("defaultPrice")]
    public string DefaultPrice { get; set; }

    /// <summary>
    /// The dialling prefix.
    /// </summary>
    [JsonProperty("dialingPrefix")]
    public string DialingPrefix { get; set; }

    /// <summary>
    /// An array of network objects
    /// </summary>
    [JsonProperty("networks")] 
    public Network[] Networks { get; set; }
}