using System.ComponentModel;

namespace Vonage.Meetings.Common;

/// <summary>
///     Defines the room type.
/// </summary>
public enum RoomType
{
    /// <summary>
    /// </summary>
    [Description("instant")] Instant,

    /// <summary>
    /// </summary>
    [Description("long_term")] LongTerm,
}