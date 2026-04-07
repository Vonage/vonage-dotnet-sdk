using Newtonsoft.Json;

namespace Vonage.Pricing;

/// <summary>
///     Represents the response from a pricing query, containing pricing information for one or more countries.
/// </summary>
public class PricingResult
{
    /// <summary>
    /// The number of countries retrieved.
    /// </summary>
    [JsonProperty("count")] 
    public string Count { get; set; }
        
    /// <summary>
    /// The code for the country you looked up: e.g. GB, US, BR, RU.
    /// </summary>
    [JsonProperty("countries")]
    public Country[] Countries { get; set; }
        
}