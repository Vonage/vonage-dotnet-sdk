using Newtonsoft.Json;
using System;

namespace Vonage.Voice
{
    public class CallResponse
    {
        /// <summary>
        ///  A unique identifier for this Call.
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// A unique identifier for the Conversation this Call is part of.
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }

        /// <summary>
        /// Possible values are outbound or inbound.
        /// </summary>
        [JsonProperty("direction")]
        public string Direction { get; set; }

        /// <summary>
        /// The status of the Call. Possible values are:
        ///  started - Platform has stared the Call.
        ///  ringing - the user's handset is ringing.
        ///  answered - the user has answered your Call.
        ///  timeout - your user did not answer your Call with ringing_timer.
        ///  machine - Platform detected an answering machine.
        ///  completed - Platform has terminated this Call.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        
    }
}
