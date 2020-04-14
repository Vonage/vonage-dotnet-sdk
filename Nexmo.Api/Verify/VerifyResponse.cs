using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyResponse
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error_text")]
        public string ErrorText { get; set; }
    }
}