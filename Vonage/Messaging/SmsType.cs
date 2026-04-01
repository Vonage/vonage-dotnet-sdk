using System.Runtime.Serialization;

namespace Vonage.Messaging;

/// <summary>
///     Represents the encoding type of an SMS message body.
/// </summary>
public enum SmsType
{
    /// <summary>
    ///     Standard text message using GSM 7-bit encoding.
    ///     Supports basic Latin characters, numbers, and common symbols.
    /// </summary>
    [EnumMember(Value = "text")]
    Text = 1,

    /// <summary>
    ///     Binary message with raw data payload.
    ///     Use with the <see cref="SendSmsRequest.Body"/> and <see cref="SendSmsRequest.Udh"/> properties.
    /// </summary>
    [EnumMember(Value = "binary")]
    Binary = 2,

    /// <summary>
    ///     Unicode message using UCS-2 encoding.
    ///     Required for messages containing non-GSM characters such as emoji, Chinese, Arabic, etc.
    /// </summary>
    [EnumMember(Value = "unicode")]
    Unicode = 4,
}