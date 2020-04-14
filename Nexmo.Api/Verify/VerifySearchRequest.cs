using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifySearchRequest
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("request_ids")]
        public string[] RequestIds { get; set; }
    }
}