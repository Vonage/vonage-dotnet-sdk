using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyControlRequest
    {
    
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("cmd")]
        public string Cmd { get; set; }
    }
}