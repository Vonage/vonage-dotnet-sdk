#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a template message on Viber.
/// </summary>
public class WhatsAppTemplateRequest : WhatsAppMessageBase
{
    /// <summary>
    ///     The message template.
    /// </summary>
    [JsonPropertyOrder(10)]
    public MessageTemplate Template { get; set; }

    /// <summary>
    ///     The WhatsApp configuration.
    /// </summary>
    [JsonPropertyName("whatsapp")]
    [JsonPropertyOrder(9)]
    public MessageWhatsApp WhatsApp { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Template;
}