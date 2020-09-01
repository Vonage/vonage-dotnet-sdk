using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice
{
    public class CallRecord
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
        /// The single or mixed collection of endpoint types you connected to.Possible value are:
        ///  phone - phone numbers in e.164 format.Options are:
        ///    number - the phone number to connect to in E.164 format.
        ///    dtmfAnswer - Set the digits that are sent to the user as soon as the Call is answered.The* and # digits are respected. You create pauses using p. Each pause is 500ms.
        /// </summary>
        [JsonProperty("to")]        
        public CallEndpoint To { get; set; }

        /// <summary>
        /// The endpoint you called from. Possible values are the same as to 
        /// </summary>
        [JsonProperty("from")]
        public CallEndpoint From { get; set; }

        /// <summary>
        /// Possible values are outbound or inbound.
        /// </summary>
        [JsonProperty("direction")]
        public string Direction { get; set; }

        /// <summary>
        /// The URL to download a call or conversation recording from.
        /// </summary>
        [JsonProperty("recording_url")]
        public string RecordingUrl { get; set; }

        /// <summary>
        /// The time the Call started
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier network used to make this Call.
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

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

        /// <summary>
        /// The price per minute for this Call.
        /// </summary>
        [JsonProperty("rate")]
        public string Rate { get; set; }

        /// <summary>
        /// The total price charged for this Call.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// The time elapsed for the Call to take place in seconds.
        /// </summary>
        [JsonProperty("duration")]
        public string Duration { get; set; }

        /// <summary>
        /// The time the Call ended
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("_links")]
        public Common.HALLinks Links { get; set; }
    }
}
