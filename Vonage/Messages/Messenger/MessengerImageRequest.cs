using Newtonsoft.Json;

namespace Vonage.Messages.Messenger
{
    public class MessengerImageRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.Messenger;
        public override MessagesMessageType MessageType => MessagesMessageType.Image;
        
        [JsonProperty("image")]
        public Attachment Image { get; set; }
        
        [JsonProperty("messenger")]
        public MessengerRequestData Data { get; set; }
    }
}