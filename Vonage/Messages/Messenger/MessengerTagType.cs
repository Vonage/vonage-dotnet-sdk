using System.ComponentModel;

namespace Vonage.Messages.Messenger;

public enum MessengerTagType
{
    [Description("CONFIRMED_EVENT_UPDATE")]
    ConfirmedEventUpdate = 0,

    [Description("POST_PURCHASE_UPDATE")] PostPurchaseUpdate = 1,

    [Description("ACCOUNT_UPDATE")] AccountUpdate = 2,

    [Description("HUMAN_AGENT")] HumanAgent = 3,

    [Description("CUSTOMER_FEEDBACK")] CustomerFeedback = 4,
}