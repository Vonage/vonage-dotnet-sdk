using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.CallStatusEvent class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.CallStatusEvent class.")]
    public class CallStatusEvent : Event
    {
        /// <summary>
        /// The number the call was made to
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// The number the call came from
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// The unique identifier for this conversation
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }

        /// <summary>
        /// Call status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Call direction, can be either inbound or outbound
        /// </summary>
        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction Direction { get; set; }
    }
}
