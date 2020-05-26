using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    public class PricingPrefixRequest
    {
        /// <summary>
        /// The numerical dialing prefix to look up pricing for. Examples include 44, 1 and so on.
        /// </summary>
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }
}