using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyCheckRequest
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }
}