using System.ComponentModel;

namespace Vonage.Meetings.Common;

/// <summary>
///     Represents the level of approval needed to join the meeting in the room.
/// </summary>
public enum RoomApprovalLevel
{
    /// <summary>
    /// </summary>
    [Description("none")] None,

    /// <summary>
    /// </summary>
    [Description("after_owner_only")] AfterOwnerOnly,

    /// <summary>
    /// </summary>
    [Description("explicit_approval")] ExplicitApproval,
}