#region
using System.ComponentModel;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
///     Defines the message tag type for Facebook Messenger. Required when using MessageTag category.
/// </summary>
public enum MessengerTagType
{
    /// <summary>
    ///     Notify the user of an update to an event they have registered for (e.g., RSVP, ticket purchase).
    /// </summary>
    [Description("CONFIRMED_EVENT_UPDATE")]
    ConfirmedEventUpdate = 0,

    /// <summary>
    ///     Notify the user of an update on a recent purchase (e.g., shipment, delivery status).
    /// </summary>
    [Description("POST_PURCHASE_UPDATE")] PostPurchaseUpdate = 1,

    /// <summary>
    ///     Notify the user of a non-recurring change to their account settings or application.
    /// </summary>
    [Description("ACCOUNT_UPDATE")] AccountUpdate = 2,

    /// <summary>
    ///     Allow human agents to respond to user inquiries. Messages can be sent within 7 days of user message.
    /// </summary>
    [Description("HUMAN_AGENT")] HumanAgent = 3,

    /// <summary>
    ///     Request feedback from the user about a recent interaction or purchase.
    /// </summary>
    [Description("CUSTOMER_FEEDBACK")] CustomerFeedback = 4,
}