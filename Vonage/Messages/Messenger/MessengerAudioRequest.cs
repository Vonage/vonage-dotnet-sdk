using Newtonsoft.Json;

namespace Vonage.Messages.Messenger
{
    public class MessengerAudioRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.Messenger;
        public override MessagesMessageType MessageType => MessagesMessageType.Audio;
        
        [JsonProperty("audio")]
        public Attachment Audio { get; set; }
        
        [JsonProperty("messenger")]
        public MessengerRequestData Data { get; set; }
    }
}