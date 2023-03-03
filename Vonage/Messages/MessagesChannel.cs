using System.ComponentModel;
using System.Runtime.Serialization;

namespace Vonage.Messages;

/// <summary>
/// </summary>
public enum MessagesChannel
{
    /// <summary>
    /// </summary>
    [EnumMember(Value = "sms")] [Description("sms")]
    SMS,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "mms")] [Description("mms")]
    MMS,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "whatsapp")] [Description("whatsapp")]
    WhatsApp,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "messenger")] [Description("messenger")]
    Messenger,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "viber_service")] [Description("viber_service")]
    ViberService,
}