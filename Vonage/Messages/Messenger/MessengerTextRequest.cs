using Newtonsoft.Json;

namespace Vonage.Messages.Messenger
{
    public class MessengerTextRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.Messenger;
        public override MessagesMessageType MessageType => MessagesMessageType.Text;
        
        /// <summary>
        /// The text of message to send; limited to 640 characters, including unicode.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("messenger")]
        public MessengerRequestData Data { get; set; }
    }
}