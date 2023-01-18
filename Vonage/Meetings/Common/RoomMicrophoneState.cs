using System.ComponentModel;

namespace Vonage.Meetings.Common;

/// <summary>
///     Defines the default microphone option for users in the pre-join screen of this room.
/// </summary>
public enum RoomMicrophoneState
{
    /// <summary>
    /// </summary>
    [Description("default")] Default,

    /// <summary>
    /// </summary>
    [Description("true")] True,

    /// <summary>
    /// </summary>
    [Description("false")] False,
}