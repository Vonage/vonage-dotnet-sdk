using Newtonsoft.Json;
using System;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.Transfer class is obsolete. " +
           "References to it should be switched to the new Vonage.Voice.EventWebhooks.Transfer class.")]
    public class Transfer : Event
    {
        /// <summary>
        /// The conversation ID that the leg was originally in
        /// </summary>
        [JsonProperty("conversation_uuid_from")]
        public string ConversationUuidFrom { get; set; }

        /// <summary>
        /// The conversation ID that the leg was transferred to
        /// </summary>
        [JsonProperty("conversation_uuid_to")]
        public string ConversationUuidTo { get; set; }
        
    }
}
