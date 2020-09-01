using Newtonsoft.Json;

namespace Vonage.Voice.EventWebhooks
{
    public class Error : EventBase
    {
        /// <summary>
        /// Information about the nature of the error
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// The unique identifier for this conversation
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
    }
}
