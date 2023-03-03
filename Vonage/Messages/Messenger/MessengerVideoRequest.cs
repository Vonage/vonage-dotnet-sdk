namespace Vonage.Messages.Messenger;

public class MessengerVideoRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    public MessengerRequestData Data { get; set; }
    public override MessagesMessageType MessageType => MessagesMessageType.Video;

    public Attachment Video { get; set; }
}