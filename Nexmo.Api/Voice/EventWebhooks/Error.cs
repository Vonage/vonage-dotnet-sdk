using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Error : EventBase
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
    }
}
