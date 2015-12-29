using Newtonsoft.Json;

namespace Nexmo.Api
{
    public class ResponseBase
    {
        [JsonProperty("error-code")]
        public string ErrorCode { get; set; }
        [JsonProperty("error-code-label")]
        public string ErrorCodeLabel { get; set; }
    }
}
