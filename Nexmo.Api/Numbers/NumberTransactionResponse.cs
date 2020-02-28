using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    public class NumberTransactionResponse
    {
        [JsonProperty("error-code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error-code-label")]
        public string ErrorCodeLabel { get; set; }
    }
}