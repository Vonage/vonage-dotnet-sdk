using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Input : Event
    {
        [JsonProperty("dtmf")]
        public string Dtmf { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
        
    }
}
