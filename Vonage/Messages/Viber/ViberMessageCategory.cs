using System.ComponentModel;

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a category tag for Viber messages.
/// </summary>
public enum ViberMessageCategory
{
    /// <summary>
    ///  Transaction.
    /// </summary>
    [Description("transaction")] Transaction = 0,

    /// <summary>
    /// Promotion.
    /// </summary>
    [Description("promotion")] Promotion = 1,
}