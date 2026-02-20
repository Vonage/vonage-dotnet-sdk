#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Sms;

/// <inheritdoc />
public class SmsRequest : MessageRequestBase
{
    /// <summary>
    ///     The channel to send to. You must provide sms in this field
    /// </summary>
    public override MessagesChannel Channel => MessagesChannel.SMS;

    /// <summary>
    ///     The type of message to send. You must provide text in this field
    /// </summary>
    public override MessagesMessageType MessageType => MessagesMessageType.Text;

    /// <summary>
    ///     The text of message to send; limited to 1000 characters. Unless unless text or unicode has been explicitly set as
    ///     the value for sms.encoding_type, the Messages API automatically detects whether unicode characters are present in
    ///     text and sends the message as appropriate as either a text or unicode SMS. For more information on concatenation
    ///     and encoding please visit: developer.vonage.com/messaging/sms/guides/concatenation-and-encoding.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     The duration in seconds the delivery of an SMS will be attempted. By default Vonage attempts delivery for 72 hours,
    ///     however the maximum effective value depends on the operator and is typically 24 - 48 hours. We recommend this value
    ///     should be kept at its default or at least 30 minutes.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(8)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int TimeToLive { get; set; }

    /// <summary>
    ///     An object of optional settings for the SMS message.
    /// </summary>
    [JsonPropertyName("sms")]
    [JsonPropertyOrder(9)]
    public OptionalSettings Settings { get; set; }

    /// <summary>
    ///     Allows to skip fraud checks on a per-message basis. The feature is feature-flagged and must be enabled for the api
    ///     key.
    /// </summary>
    [JsonIgnore]
    [Obsolete("Favor 'TrustedRecipient' instead.")]
    public bool TrustedNumber { get; set; }

    /// <summary>
    ///     Setting this parameter to true overrides, on a per-message basis, any protections set up via Fraud Defender
    ///     (Traffic Rules, SMS Burst Protection, AIT Protection). This parameter only has any effect for accounts subscribed
    ///     to Fraud Defender Premium.
    /// </summary>
    [JsonPropertyOrder(10)]
    [JsonPropertyName("trusted_recipient")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool TrustedRecipient { get; set; }

    /// <summary>
    ///     Sets the Id of the number pool managing the number.
    /// </summary>
    [JsonPropertyOrder(11)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string PoolId { get; set; }
}

/// <summary>
///     An object of optional settings for the SMS message.
/// </summary>
/// <param name="EncodingType">
///     The encoding type to use for the message. If set to either text or unicode the specified
///     type will be used. If set to auto (the default), the Messages API will automatically set the type based on the
///     content of text; i.e. if unicode characters are detected in text, then the message will be encoded as unicode, and
///     otherwise as text.
/// </param>
/// <param name="ContentId">
///     A string parameter that satisfies regulatory requirements when sending an SMS to specific
///     countries. For more information please refer to the Country-Specific Outbound SMS Features"
/// </param>
/// <param name="EntityId">
///     A string parameter that satisfies regulatory requirements when sending an SMS to specific
///     countries. For more information please refer to the Country-Specific Outbound SMS Features
/// </param>
public record OptionalSettings(string EncodingType, string ContentId, string EntityId);