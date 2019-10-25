using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class CreateTextEventResponse
    {
        [JsonProperty("body")]
        public TextEventBody Body { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        public class TextEventBody
        {
            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
