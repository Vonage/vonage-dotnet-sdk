using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class StandardInsightResponse : BasicInsightResponse
    {
        [JsonProperty("request_price")]
        public string RequestPrice { get; set; }

        [JsonProperty("refund_price")]
        public string RefundPrice { get; set; }

        [JsonProperty("remaining_balance")]
        public string RemainingBalance { get; set; }

        [JsonProperty("current_carrier")]
        public Carrier CurrentCarrier { get; set; }

        [JsonProperty("original_carrier")]
        public Carrier OriginalCarrier { get; set; }

        [JsonProperty("ported")]
        public string Ported { get; set; }

        [JsonProperty("roaming")]
        public Roaming Roaming { get; set; }

        [JsonProperty("caller_identity")]
        public CallerId CallerIdentity { get; set; }

        [JsonProperty("caller_name")]
        public string CallerName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("caller_type")]
        public string CallerType { get; set; }
    }
}