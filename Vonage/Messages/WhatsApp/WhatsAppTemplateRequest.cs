using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppTemplateRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;
    public override MessagesMessageType MessageType => MessagesMessageType.Template;

    [JsonPropertyOrder(7)] public MessageTemplate Template { get; set; }

    [JsonPropertyName("whatsapp")]
    [JsonPropertyOrder(6)]
    public MessageWhatsApp WhatsApp { get; set; }
}