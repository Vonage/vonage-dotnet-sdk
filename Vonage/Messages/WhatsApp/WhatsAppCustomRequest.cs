using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class WhatsAppCustomRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.WhatsApp;
        public override MessagesMessageType MessageType => MessagesMessageType.Custom;

        /// <summary>
        /// A custom payload, which is passed directly to WhatsApp for certain features such as
        /// templates and interactive messages.
        /// </summary>
        [JsonProperty("custom")]
        public object Custom { get; set; }
    }
}