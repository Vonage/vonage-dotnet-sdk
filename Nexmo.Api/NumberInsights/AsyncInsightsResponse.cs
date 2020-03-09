using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class AsyncInsightsResponse
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("remaining_balance")]
        public string RemainingBalance { get; set; }

        [JsonProperty("request_price")]
        public string RequestPrice { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}