using System.Text.Json.Serialization;

namespace Vonage.Messages.Viber;

public class ViberImageRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.ViberService;

    public ViberRequestData Data { get; set; }

    /// <summary>
    ///     The URL of the image attachment.
    /// </summary>
    [JsonPropertyOrder(6)]
    public Attachment Image { get; set; }

    public override MessagesMessageType MessageType => MessagesMessageType.Image;
}