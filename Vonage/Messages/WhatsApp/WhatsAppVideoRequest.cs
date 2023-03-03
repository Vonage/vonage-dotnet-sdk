using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppVideoRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;
    public override MessagesMessageType MessageType => MessagesMessageType.Video;

    /// <summary>
    ///     The video attachment.
    ///     Supports .mp4 and .3gpp. Note, only H.264 video codec and AAC audio codec is supported.
    /// </summary>
    [JsonPropertyOrder(6)]
    public CaptionedAttachment Video { get; set; }
}