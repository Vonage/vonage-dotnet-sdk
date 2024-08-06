#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a template message on Viber.
/// </summary>
public struct WhatsAppTemplateRequest : IWhatsAppMessage
{
    /// <summary>
    ///     The message template.
    /// </summary>
    [JsonPropertyOrder(6)]
    public MessageTemplate Template { get; set; }

    /// <summary>
    ///     The WhatsApp configuration.
    /// </summary>
    [JsonPropertyName("whatsapp")]
    [JsonPropertyOrder(5)]
    public MessageWhatsApp WhatsApp { get; set; }

    /// <summary>
    ///     An optional context used for quoting/replying to a specific message in a conversation. When used, the WhatsApp UI
    ///     will display the new message along with a contextual bubble that displays the quoted/replied to message's content.
    /// </summary>
    [JsonPropertyOrder(6)]
    public WhatsAppContext Context { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Template;

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(7)]
    public string WebhookVersion { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(8)]
    public Uri WebhookUrl { get; set; }
}