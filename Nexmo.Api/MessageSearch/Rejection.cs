using Newtonsoft.Json;

namespace Nexmo.Api.MessageSearch
{
    public class Rejection
    {
        [JsonProperty("account-id")]
        public string AccountId { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("date-received")]
        public string DateReceived { get; set; }

        [JsonProperty("error-code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error-code-label")]
        public string ErrorCodeLabel { get; set; }
        
    }
}