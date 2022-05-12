using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class WhatsAppTemplateRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.WhatsApp;
        public override MessagesMessageType MessageType => MessagesMessageType.Template;

        [JsonProperty("template")]
        public MessageTemplate Template { get; set; }

        [JsonProperty("whatsapp")]
        public MessageWhatsApp WhatsApp { get; set; }
    }
}