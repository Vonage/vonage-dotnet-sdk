#region
using System;
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
    MessagesChannel Channel { get; }

    /// <summary>
    ///     Client reference of up to 40 characters. The reference will be present in every message status.
    /// </summary>
    string ClientRef { get; set; }

    /// <summary>
    ///     The phone number of the message sender in the E.164 format. Don't use a leading + or 00 when entering a phone
    ///     number,
    ///     start with the country code, for example, 447700900000. For SMS in certain localities alpha-numeric sender id's
    ///     will
    ///     work as well.
    /// </summary>
    string From { get; set; }

    /// <summary>
    ///     The type of message to send.
    /// </summary>
    MessagesMessageType MessageType { get; }

    /// <summary>
    ///     The phone number of the message recipient in the E.164 format. Don't use a leading + or 00 when entering a phone
    ///     number,
    ///     start with the country code, for example, 447700900000.
    /// </summary>
    string To { get; set; }

    /// <summary>
    ///     Specifies which version of the Messages API will be used to send Status Webhook messages for this particular
    ///     message. For example, if v0.1 is set, then the JSON body of Status Webhook messages for this message will be sent
    ///     in Messages v0.1 format. Over-rides account-level and application-level API version settings on a per-message
    ///     basis.
    /// </summary>
    string WebhookVersion { get; set; }

    /// <summary>
    ///     Specifies the URL to which Status Webhook messages will be sent for this particular message. Over-rides
    ///     account-level and application-level Status Webhook url settings on a per-message basis.
    /// </summary>
    Uri WebhookUrl { get; set; }
}