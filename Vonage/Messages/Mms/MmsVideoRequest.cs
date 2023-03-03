using System.Text.Json.Serialization;

namespace Vonage.Messages.Mms;

public class MmsVideoRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.MMS;

    public override MessagesMessageType MessageType => MessagesMessageType.Video;
    [JsonPropertyOrder(6)] public CaptionedAttachment Video { get; set; }
}