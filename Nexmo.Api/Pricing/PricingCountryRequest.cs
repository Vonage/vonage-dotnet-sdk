using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
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