using Newtonsoft.Json;

namespace Nexmo.Api.Voice.AnswerWebhooks
{
    public class Answer
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
