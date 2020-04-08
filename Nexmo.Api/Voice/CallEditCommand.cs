using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice
{
    public class CallEditCommand
    {
        public enum ActionType
        {
            hangup=1,
            mute=2,
            unmute=3,
            earmuff=4,
            unearmuff=5,
            transfer=6
        }
        /// <summary>
        /// Use one of the following options to modify the call:
        /// hangup    Terminates this call leg.
        /// mute      Mutes this call leg.
        /// unmute    Unmutes this call leg.
        /// earmuff   Prevents the recipient of this call leg from hearing other parts of the conversation.
        /// unearmuff Removes the earmuff effect from this call leg.
        /// transfer  Transfers this call leg to another NCCO, as specified by the destination parameter.
        /// </summary>
        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]        
        public ActionType Action { get; set; }
        /// <summary>
        /// Optional. A JSON object pointing to a replacement NCCO, when action is transfer.
        /// </summary>        
        [JsonProperty("destination")]
        public Destination Destination { get; set; }

    }
}
