using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class AccountSettingsResult
    {
        [JsonProperty("mo-callback-url")]
        public string MoCallbackUrl { get; set; }
        [JsonProperty("dr-callback-url ")]
        public string DrCallbackurl { get; set; }
        [JsonProperty("max-outbound-request ")]
        public decimal MaxOutboundRequest { get; set; }
        [JsonProperty("max-inbound-request ")]
        public decimal MaxInboundRequest { get; set; }
        [JsonProperty("max-calls-per-second ")]
        public decimal MaxCallsPerSecond { get; set; }
    }
}