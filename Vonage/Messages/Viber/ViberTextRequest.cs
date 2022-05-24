using Newtonsoft.Json;

namespace Vonage.Messages.Viber
{
    
    public class ViberTextRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.ViberService;
        public override MessagesMessageType MessageType => MessagesMessageType.Text;
        
        /// <summary>
        /// The text of message to send; limited to 640 characters, including unicode.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("viber_service")]
        public ViberRequestData Data { get; set; }
    }
}