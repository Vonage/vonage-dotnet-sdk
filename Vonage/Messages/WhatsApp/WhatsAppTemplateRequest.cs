using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a template message on Viber.
/// </summary>
public struct WhatsAppTemplateRequest : IWhatsAppMessage
{
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

    /// <summary>
    ///     The message template.
    /// </summary>
    [JsonPropertyOrder(6)]
    public MessageTemplate Template { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }

    /// <summary>
    ///     The WhatsApp configuration.
    /// </summary>
    [JsonPropertyName("whatsapp")]
    [JsonPropertyOrder(5)]
    public MessageWhatsApp WhatsApp { get; set; }
}