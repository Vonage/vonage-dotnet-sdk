using System.Runtime.Serialization;

namespace Vonage.Messages.Messenger
{
    public enum MessengerTagType
    {
        [EnumMember(Value = "CONFIRMED_EVENT_UPDATE")]
        ConfirmedEventUpdate = 0,
        
        [EnumMember(Value = "POST_PURCHASE_UPDATE")]
        PostPurchaseUpdate = 1,
        
        [EnumMember(Value = "ACCOUNT_UPDATE")]
        AccountUpdate = 2,
        
        [EnumMember(Value = "HUMAN_AGENT")]
        HumanAgent = 3,
        
        [EnumMember(Value = "CUSTOMER_FEEDBACK")]
        CustomerFeedback = 4
    }
}