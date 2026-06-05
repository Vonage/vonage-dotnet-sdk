#region
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages;

/// <summary>
///     Represents a message to be sent by an IMessageClient.
/// </summary>
public interface IMessage
{
    /// <summary>
    ///     The channel to send to.
    /// </summary>
    [JsonPropertyOrder(0)]
    MessagesChannel Channel { get; }

    /// <summary>
    ///     Client reference of up to 40 characters. The reference will be present in every message status.
    /// </summary>
    [JsonPropertyOrder(5)]
    string ClientRef { get; set; }

    /// <summary>
    ///     The sender of the message. The accepted format depends on the channel
    ///     of the concrete implementation (see remarks).
    /// </summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item><description>
    ///             SMS, MMS, WhatsApp: a phone number in E.164 format, without a leading + or 00.
    ///             Start with the country code, e.g. 447700900001. For SMS, alphanumeric sender
    ///             IDs are supported in certain localities.
    ///         </description></item>
    ///         <item><description>
    ///             RCS: an alphanumeric Sender ID that exactly matches the one registered during
    ///             onboarding. Spaces are not permitted, e.g. Vonage.
    ///         </description></item>
    ///         <item><description>
    ///             Messenger, Viber: the sender ID (1–50 characters).
    ///         </description></item>
    ///         <item><description>
    ///             Email: the sender email address whose domain must be verified by Vonage before
    ///             sending. A display name may be included using the format
    ///             "Display Name &lt;your.address@yourdomain.com&gt;".
    ///         </description></item>
    ///     </list>
    /// </remarks>
    [JsonPropertyOrder(4)]
    string From { get; set; }

    /// <summary>
    ///     The type of message to send.
    /// </summary>
    [JsonPropertyOrder(1)]
    MessagesMessageType MessageType { get; }

    /// <summary>
    ///     The recipient of the message. The accepted format depends on the channel
    ///     of the concrete implementation (see remarks).
    /// </summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item><description>
    ///             SMS, MMS, RCS, Viber: a phone number in E.164 format (7–15 digits), without a
    ///             leading + or 00. Start with the country code, e.g. 447700900000.
    ///         </description></item>
    ///         <item><description>
    ///             WhatsApp: a phone number in E.164 format without a leading +, or a WhatsApp
    ///             Business-scoped User ID (BSUID), e.g. US.13491208655302741918.
    ///         </description></item>
    ///         <item><description>
    ///             Messenger: the recipient ID (1–50 characters).
    ///         </description></item>
    ///         <item><description>
    ///             Email: the recipient email address, e.g. user@example.com.
    ///         </description></item>
    ///     </list>
    /// </remarks>
    [JsonPropertyOrder(3)]
    string To { get; set; }

    /// <summary>
    ///     Specifies which version of the Messages API will be used to send Status Webhook messages for this particular
    ///     message. For example, if v0.1 is set, then the JSON body of Status Webhook messages for this message will be sent
    ///     in Messages v0.1 format. Over-rides account-level and application-level API version settings on a per-message
    ///     basis.
    /// </summary>
    [JsonPropertyOrder(6)]
    string WebhookVersion { get; set; }

    /// <summary>
    ///     Specifies the URL to which Status Webhook messages will be sent for this particular message. Over-rides
    ///     account-level and application-level Status Webhook url settings on a per-message basis.
    /// </summary>
    [JsonPropertyOrder(7)]
    Uri WebhookUrl { get; set; }

    /// <summary>
    ///     Gets or sets the failover messages to be sent if the primary message fails.
    /// </summary>
    [JsonPropertyOrder(99)]
    List<IMessage> Failover { get; set; }

    /// <summary>
    ///     Returns any validation errors for this message. An empty collection means the message is valid.
    /// </summary>
    /// <returns>A collection of error messages.</returns>
    IEnumerable<string> GetErrors();

    /// <summary>
    ///     Serializes the message.
    /// </summary>
    /// <returns>The serialized message.</returns>
    string Serialize();
}