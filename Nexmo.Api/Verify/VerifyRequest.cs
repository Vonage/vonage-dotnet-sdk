using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyRequest
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("sender_id")]
        public string SenderId { get; set; }

        [JsonProperty("code_length")]
        public int CodeLength { get; set; }

        [JsonProperty("lg")]
        public string Lg { get; set; }

        [JsonProperty("pin_expiry")]
        public int PinExpiry { get; set; }

        [JsonProperty("next_event_wait")]
        public int NextEventWait { get; set; }

        [JsonProperty("workflow_id")]
        public string WorkflowId { get; set; }
    }
}