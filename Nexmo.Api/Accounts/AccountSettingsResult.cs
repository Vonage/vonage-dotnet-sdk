using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class AccountSettingsResult
    {
        /// <summary>
        /// The current or updated inbound message URI
        /// </summary>
        [JsonProperty("mo-callback-url")]
        public string MoCallbackUrl { get; set; }

        /// <summary>
        /// The current or updated delivery receipt URI
        /// </summary>
        [JsonProperty("dr-callback-url")]
        public string DrCallbackurl { get; set; }

        /// <summary>
        /// The maximum number of outbound messages per second.
        /// </summary>
        [JsonProperty("max-outbound-request")]
        public decimal MaxOutboundRequest { get; set; }

        /// <summary>
        /// The maximum number of inbound messages per second.
        /// </summary>
        [JsonProperty("max-inbound-request")]
        public decimal MaxInboundRequest { get; set; }

        /// <summary>
        /// The maximum number of API calls per second.
        /// </summary>
        [JsonProperty("max-calls-per-second")]
        public decimal MaxCallsPerSecond { get; set; }
    }
}