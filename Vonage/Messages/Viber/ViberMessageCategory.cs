using System.ComponentModel;

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a category tag for Viber messages.
/// </summary>
public enum ViberMessageCategory
{
    /// <summary>
    ///     sssssss
    /// </summary>
    [Description("transaction")] Transaction = 0,

    /// <summary>
    /// </summary>
    [Description("promotion")] Promotion = 1,
}