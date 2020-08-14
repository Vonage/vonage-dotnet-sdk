using System;
using Newtonsoft.Json;

namespace Vonage.Verify
{
    public class VerifySearchResponse
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("sender_id")]
        public string SenderId { get; set; }

        [JsonProperty("date_submitted")]
        public string DateSubmitted { get; set; }

        [JsonProperty("date_finalized")]
        public string DateFinalized { get; set; }

        [JsonProperty("first_event_date")]
        public string FirstEventDate { get; set; }

        [JsonProperty("last_event_date")]
        public string LastEventDate { get; set; }

        [JsonProperty("checks")]
        public VerifyCheck[] Checks { get; set; }

        [JsonProperty("events")]
        public VerifyEvent[] Events { get; set; }

        [JsonProperty("estimated_price_messages_sent")]
        public string EstimatedPriceMessagesSent { get; set; }
    }
}