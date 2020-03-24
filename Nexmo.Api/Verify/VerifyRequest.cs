using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyRequest
    {
        public enum Workflow
        {
            SMS_TTS_TTS=1,
            SMS_SMS_TTS=1,
            TTS_TTS=3,
            SMS_SMS=4,
            SMS_TTS=5,
            SMS=6,
            TTS=7
        }
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
        public Workflow? WorkflowId { get; set; }
    }
}