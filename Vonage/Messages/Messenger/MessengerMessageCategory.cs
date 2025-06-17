#region
using System.ComponentModel;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
/// </summary>
public enum MessengerMessageCategory
{
    /// <summary>
    /// </summary>
    [Description("response")] Response = 0,

    /// <summary>
    /// </summary>
    [Description("update")] Update = 1,

    /// <summary>
    /// </summary>
    [Description("message_tag")] MessageTag = 2,
}