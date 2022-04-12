using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice
{
    public class CallEditCommand
    {
        public enum ActionType
        {
            [EnumMember(Value = "hangup")]
            hangup = 1,
            [EnumMember(Value = "mute")]
            mute = 2,
            [EnumMember(Value = "unmute")]
            unmute = 3,
            [EnumMember(Value = "earmuff")]
            earmuff = 4,
            [EnumMember(Value = "unearmuff")]
            unearmuff = 5,
            [EnumMember(Value = "transfer")]
            transfer = 6
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
