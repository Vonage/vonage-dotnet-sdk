using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyResponse : VerifyResponseBase
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }
}