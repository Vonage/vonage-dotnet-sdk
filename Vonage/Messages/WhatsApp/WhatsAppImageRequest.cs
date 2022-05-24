using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class WhatsAppImageRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.WhatsApp;
        public override MessagesMessageType MessageType => MessagesMessageType.Image;

        /// <summary>
        /// The image attachment. Supports .jpg, .jpeg, and .png.
        /// </summary>
        [JsonProperty("image")]
        public CaptionedAttachment Image { get; set; }
    }
}