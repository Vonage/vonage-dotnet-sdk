using System.Text.Json.Serialization;

namespace Vonage.Messages.Messenger;

public class MessengerFileRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    public MessengerRequestData Data { get; set; }

    [JsonPropertyOrder(6)] public Attachment File { get; set; }
    public override MessagesMessageType MessageType => MessagesMessageType.File;
}