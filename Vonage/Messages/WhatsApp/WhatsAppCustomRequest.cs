using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppCustomRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <summary>
    ///     A custom payload, which is passed directly to WhatsApp for certain features such as
    ///     templates and interactive messages.
    /// </summary>
    [JsonPropertyOrder(6)]
    public object Custom { get; set; }

    public override MessagesMessageType MessageType => MessagesMessageType.Custom;
}