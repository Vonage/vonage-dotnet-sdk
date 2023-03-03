using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppImageRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <summary>
    ///     The image attachment. Supports .jpg, .jpeg, and .png.
    /// </summary>
    [JsonPropertyOrder(6)]
    public CaptionedAttachment Image { get; set; }

    public override MessagesMessageType MessageType => MessagesMessageType.Image;
}