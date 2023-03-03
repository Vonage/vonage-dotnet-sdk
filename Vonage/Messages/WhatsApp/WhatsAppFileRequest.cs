using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppFileRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <summary>
    ///     The file attachment.
    ///     Supports supports a wide range of attachments including .zip, .csv and .pdf.
    /// </summary>
    [JsonPropertyOrder(6)]
    public CaptionedAttachment File { get; set; }

    public override MessagesMessageType MessageType => MessagesMessageType.File;
}