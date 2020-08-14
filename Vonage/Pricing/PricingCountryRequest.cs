using Newtonsoft.Json;

namespace Vonage.Pricing
{
    public class PricingCountryRequest
    {
        /// <summary>
        /// A two letter country code. For example, CA.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}