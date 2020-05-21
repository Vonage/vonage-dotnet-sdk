using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class CallStatusEvent : Event
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction Direction { get; set; }
    }
}
