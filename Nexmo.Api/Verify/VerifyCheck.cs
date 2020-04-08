using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyCheck
    {
        [JsonProperty("date_received")]
        public string DateReceived { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }
}