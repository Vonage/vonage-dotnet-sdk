using Newtonsoft.Json;

namespace Vonage.Pricing;

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