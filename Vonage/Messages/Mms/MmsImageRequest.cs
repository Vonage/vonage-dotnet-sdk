using System.Text.Json.Serialization;

namespace Vonage.Messages.Mms;

public class MmsImageRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.MMS;
    [JsonPropertyOrder(6)] public Attachment Image { get; set; }

    public override MessagesMessageType MessageType => MessagesMessageType.Image;
}