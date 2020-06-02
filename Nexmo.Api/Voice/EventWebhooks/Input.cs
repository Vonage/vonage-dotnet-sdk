using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Input : Event
    {
        /// <summary>
        /// The buttons pressed by the user
        /// </summary>
        [JsonProperty("dtmf")]
        public string Dtmf { get; set; }

        /// <summary>
        /// Whether the input action timed out: true if it did, false if not
        /// </summary>
        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        /// <summary>
        /// The unique identifier for this conversation
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }

        /// <summary>
        /// The number the call came from
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// The number the call was made to
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }
        
        [JsonProperty("speech")]
        public SpeechResult SpeechResult { get; set; }
        
    }
}
