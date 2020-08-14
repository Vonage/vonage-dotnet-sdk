using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.Error class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.Error class.")]
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
