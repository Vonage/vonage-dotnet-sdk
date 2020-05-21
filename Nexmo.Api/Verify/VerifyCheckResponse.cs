using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyCheckResponse : VerifyResponseBase
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("estimated_price_messages_sent")]
        public string EstimatedPriceMessagesSent { get; set; }        
    }
}