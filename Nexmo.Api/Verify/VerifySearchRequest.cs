using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifySearchRequest
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }    
}