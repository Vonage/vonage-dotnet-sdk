#region
using System.ComponentModel;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
/// </summary>
public enum MessengerTagType
{
    /// <summary>
    /// </summary>
    [Description("CONFIRMED_EVENT_UPDATE")]
    ConfirmedEventUpdate = 0,

    /// <summary>
    /// </summary>
    [Description("POST_PURCHASE_UPDATE")] PostPurchaseUpdate = 1,

    /// <summary>
    /// </summary>
    [Description("ACCOUNT_UPDATE")] AccountUpdate = 2,

    /// <summary>
    /// </summary>
    [Description("HUMAN_AGENT")] HumanAgent = 3,

    /// <summary>
    /// </summary>
    [Description("CUSTOMER_FEEDBACK")] CustomerFeedback = 4,
}