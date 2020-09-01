using Newtonsoft.Json;
using Nexmo.Api.Voice.EventWebhooks;

namespace Nexmo.Api.Voice.AnswerWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.AnswerWebhooks.Answer class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.AnswerWebhooks.Answer class.")]
    public class Answer : EventBase
    {
        /// <summary>
        /// The number the call came from (this could be your Nexmo number if the call is started programmatically)
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// The call the number is to (this could be a Nexmo number or another phone number)
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// A unique identifier for this call
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// A unique identifier for this conversation
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }

    }
}
