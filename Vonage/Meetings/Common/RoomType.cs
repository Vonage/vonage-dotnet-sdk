using System.ComponentModel;

namespace Vonage.Meetings.Common;

/// <summary>
///     Defines the room type.
/// </summary>
public enum RoomType
{
    /// <summary>
    /// An instant is active for 10 minutes until the first participant joins the roo, and remains active for 10 minutes after the last participant leaves
    /// </summary>
    [Description("instant")] Instant,

    /// <summary>
    /// A long term room expires after a specific date
    /// </summary>
    [Description("long_term")] LongTerm,
}