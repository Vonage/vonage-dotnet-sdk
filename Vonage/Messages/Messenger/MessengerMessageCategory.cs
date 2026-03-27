#region
using System.ComponentModel;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
///     Defines the message category for Facebook Messenger, which maps to Facebook's messaging_type.
/// </summary>
public enum MessengerMessageCategory
{
    /// <summary>
    ///     Response to a user-initiated conversation. Must be sent within 24 hours of the user's message.
    /// </summary>
    [Description("response")] Response = 0,

    /// <summary>
    ///     Proactive message sent outside the 24-hour window. Requires special permissions.
    /// </summary>
    [Description("update")] Update = 1,

    /// <summary>
    ///     Message with a specific tag. Requires a corresponding Tag value to be set.
    /// </summary>
    [Description("message_tag")] MessageTag = 2,
}