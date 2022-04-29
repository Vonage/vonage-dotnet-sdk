using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class WhatsAppTextRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.WhatsApp;
        public override MessagesMessageType MessageType => MessagesMessageType.Text;

        /// <summary>
        /// The text of message to send; limited to 4096 characters, including unicode.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}