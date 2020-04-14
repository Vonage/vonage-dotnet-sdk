using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    public class PricingCountryRequest
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}