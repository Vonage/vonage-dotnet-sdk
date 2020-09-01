using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    [System.Obsolete("The Nexmo.Api.Pricing.PricingPrefixRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Pricing.PricingPrefixRequest class.")]
    public class PricingPrefixRequest
    {
        /// <summary>
        /// The numerical dialing prefix to look up pricing for. Examples include 44, 1 and so on.
        /// </summary>
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }
}