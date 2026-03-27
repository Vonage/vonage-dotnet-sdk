#region
using System.ComponentModel;
#endregion

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a category tag for Viber messages.
/// </summary>
public enum ViberMessageCategory
{
    /// <summary>
    ///     Transactional messages such as order confirmations, shipping notifications, and account updates.
    ///     The first message sent to a user must be transactional.
    /// </summary>
    [Description("transaction")] Transaction = 0,

    /// <summary>
    ///     Promotional messages such as marketing offers and advertisements. Can only be sent after
    ///     establishing a transactional relationship with the user.
    /// </summary>
    [Description("promotion")] Promotion = 1,
}