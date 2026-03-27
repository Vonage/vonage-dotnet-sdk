#region
using System.ComponentModel;
using System.Runtime.Serialization;
#endregion

// ReSharper disable InconsistentNaming

namespace Vonage.Messages;

/// <summary>
///     Defines the messaging channel to use for sending a message.
/// </summary>
public enum MessagesChannel
{
    /// <summary>
    ///     Short Message Service. Basic text messaging available worldwide.
    /// </summary>
    [EnumMember(Value = "sms")] [Description("sms")]
    SMS,

    /// <summary>
    ///     Multimedia Messaging Service. Supports images, audio, video, and files. Available in US only.
    /// </summary>
    [EnumMember(Value = "mms")] [Description("mms")]
    MMS,

    /// <summary>
    ///     WhatsApp Business messaging. Supports text, media, templates, and interactive messages.
    /// </summary>
    [EnumMember(Value = "whatsapp")] [Description("whatsapp")]
    WhatsApp,

    /// <summary>
    ///     Facebook Messenger. Supports text, images, audio, video, and files.
    /// </summary>
    [EnumMember(Value = "messenger")] [Description("messenger")]
    Messenger,

    /// <summary>
    ///     Viber Business Messages. Supports text, images, video, and files.
    /// </summary>
    [EnumMember(Value = "viber_service")] [Description("viber_service")]
    ViberService,

    /// <summary>
    ///     Rich Communication Services. Next-generation SMS with rich media and interactive features.
    /// </summary>
    [EnumMember(Value = "rcs")] [Description("rcs")]
    RCS,
}