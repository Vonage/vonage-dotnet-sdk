using Newtonsoft.Json;

namespace Vonage.Verify
{
    public class VerifyResponse : VerifyResponseBase
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }
}