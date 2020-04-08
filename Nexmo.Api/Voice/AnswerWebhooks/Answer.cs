using Newtonsoft.Json;
using Nexmo.Api.Voice.EventWebhooks;

namespace Nexmo.Api.Voice.AnswerWebhooks
{
    public class Answer : EventBase
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }

    }
}
