using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    public class PricingPrefixRequest
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }
}