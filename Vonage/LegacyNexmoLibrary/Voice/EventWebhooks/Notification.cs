using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.Notification class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.Notification class.")]
    public class Notification<T> : EventBase
    {
        /// <summary>
        /// Custom payload of for the notification action
        /// </summary>
        [JsonProperty("payload")]
        public T Payload { get; set; }

        /// <summary>
        /// A unique identifier for this conversation
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
    }
}
