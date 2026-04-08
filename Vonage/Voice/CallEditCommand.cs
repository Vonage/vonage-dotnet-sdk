using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice;

/// <summary>
///     Represents a command to modify an in-progress call (hangup, mute, unmute, earmuff, unearmuff, or transfer).
/// </summary>
public class CallEditCommand
{
    /// <summary>
    ///     Defines the actions that can be performed on an in-progress call.
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        ///     Terminates this call leg.
        /// </summary>
        [EnumMember(Value = "hangup")]
        hangup = 1,

        /// <summary>
        ///     Mutes this call leg so the participant cannot be heard by others.
        /// </summary>
        [EnumMember(Value = "mute")]
        mute = 2,

        /// <summary>
        ///     Unmutes this call leg, restoring the participant's audio to others.
        /// </summary>
        [EnumMember(Value = "unmute")]
        unmute = 3,

        /// <summary>
        ///     Prevents this call leg's participant from hearing other parts of the conversation.
        /// </summary>
        [EnumMember(Value = "earmuff")]
        earmuff = 4,

        /// <summary>
        ///     Removes the earmuff effect, allowing the participant to hear the conversation again.
        /// </summary>
        [EnumMember(Value = "unearmuff")]
        unearmuff = 5,

        /// <summary>
        ///     Transfers this call leg to a new NCCO specified by the <see cref="CallEditCommand.Destination"/> property.
        /// </summary>
        [EnumMember(Value = "transfer")]
        transfer = 6,
    }

    /// <summary>
    ///     The action to perform on the call (hangup, mute, unmute, earmuff, unearmuff, or transfer).
    /// </summary>
    [JsonProperty("action")]
    [JsonConverter(typeof(StringEnumConverter))]
    public ActionType Action { get; set; }

    /// <summary>
    ///     The transfer destination containing either an inline NCCO or an answer URL. Required when <see cref="Action"/> is set to <see cref="ActionType.transfer"/>.
    /// </summary>
    [JsonProperty("destination")]
    public Destination Destination { get; set; }
}