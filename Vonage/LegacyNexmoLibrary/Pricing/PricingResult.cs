using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    [System.Obsolete("The Nexmo.Api.Pricing.PricingResult class is obsolete. " +
        "References to it should be switched to the new Vonage.Pricing.PricingResult class.")]
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
}