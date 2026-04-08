#region
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Voice.Nccos;

/// <summary>
///     Base class for all Nexmo Call Control Object (NCCO) actions. Each action represents a step in the call flow such as speaking text, playing audio, recording, or connecting to an endpoint.
/// </summary>
public abstract class NccoAction
{
    /// <summary>
    ///     Defines the available NCCO action types that control call flow behavior.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActionType
    {
        /// <summary>
        ///     Records the call audio. The recording is available for download via the recording webhook.
        /// </summary>
        [EnumMember(Value = "record")] Record = 1,

        /// <summary>
        ///     Creates or joins a named conversation, enabling multi-party audio conferencing.
        /// </summary>
        [EnumMember(Value = "conversation")] Conversation = 2,

        /// <summary>
        ///     Connects the call to another endpoint such as a phone number, SIP URI, WebSocket, or app user.
        /// </summary>
        [EnumMember(Value = "connect")] Connect = 3,

        /// <summary>
        ///     Plays a text-to-speech message to the caller using the specified language and voice style.
        /// </summary>
        [EnumMember(Value = "talk")] Talk = 4,

        /// <summary>
        ///     Streams an audio file to the caller from a URL.
        /// </summary>
        [EnumMember(Value = "stream")] Stream = 5,

        /// <summary>
        ///     Collects user input via DTMF keypad tones or automatic speech recognition (ASR).
        /// </summary>
        [EnumMember(Value = "input")] Input = 6,

        /// <summary>
        ///     Sends a webhook notification to a specified URL with a custom payload, without interrupting the call flow.
        /// </summary>
        [EnumMember(Value = "notify")] Notify = 7,

        /// <summary>
        ///     Pauses the call flow for a specified duration before proceeding to the next action.
        /// </summary>
        [EnumMember(Value = "wait")] Wait = 8,

        /// <summary>
        ///     Transfers the call to a new NCCO, either inline or fetched from an answer URL.
        /// </summary>
        [EnumMember(Value = "transfer")] Transfer = 9,
    }

    /// <summary>
    ///     The type of NCCO action to execute.
    /// </summary>
    [JsonProperty("action")] public abstract ActionType Action { get; }
}