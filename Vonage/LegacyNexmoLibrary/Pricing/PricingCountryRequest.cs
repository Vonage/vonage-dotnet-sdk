using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    [System.Obsolete("The Nexmo.Api.Pricing.PricingCountryRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Pricing.PricingCountryRequest class.")]
    public class PricingCountryRequest
    {
        /// <summary>
        /// A two letter country code. For example, CA.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}