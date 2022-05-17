using Newtonsoft.Json;

namespace Vonage.Messages.Messenger
{
    public class MessengerVideoRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.Messenger;
        public override MessagesMessageType MessageType => MessagesMessageType.Video;
        
        [JsonProperty("video")]
        public Attachment Video { get; set; }
        
        [JsonProperty("messenger")]
        public MessengerRequestData Data { get; set; }
    }
}