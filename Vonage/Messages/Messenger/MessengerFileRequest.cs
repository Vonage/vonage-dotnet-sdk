using Newtonsoft.Json;

namespace Vonage.Messages.Messenger
{
    public class MessengerFileRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.Messenger;
        public override MessagesMessageType MessageType => MessagesMessageType.File;
        
        [JsonProperty("file")]
        public Attachment File { get; set; }
        
        [JsonProperty("messenger")]
        public MessengerRequestData Data { get; set; }
    }
}