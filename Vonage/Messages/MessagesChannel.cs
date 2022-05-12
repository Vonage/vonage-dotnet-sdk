using System.Runtime.Serialization;

namespace Vonage.Messages
{
    public enum MessagesChannel
    {
        [EnumMember(Value = "sms")]
        SMS,

        [EnumMember(Value = "mms")]
        MMS,

        [EnumMember(Value = "whatsapp")]
        WhatsApp,

        [EnumMember(Value = "messenger")]
        Messenger,

        [EnumMember(Value = "viber_service")]
        ViberService
    }
}