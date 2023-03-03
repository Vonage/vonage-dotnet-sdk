using System.Text.Json.Serialization;

namespace Vonage.Messages.Messenger;

public class MessengerImageRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    public MessengerRequestData Data { get; set; }

    [JsonPropertyOrder(6)] public Attachment Image { get; set; }
    public override MessagesMessageType MessageType => MessagesMessageType.Image;
}