using System.Text.Json.Serialization;

namespace Vonage.Messages.Messenger;

public class MessengerAudioRequest : MessageRequestBase
{
    [JsonPropertyOrder(6)] public Attachment Audio { get; set; }
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    public MessengerRequestData Data { get; set; }
    public override MessagesMessageType MessageType => MessagesMessageType.Audio;
}