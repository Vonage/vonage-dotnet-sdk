using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class WhatsAppFileRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.WhatsApp;
        public override MessagesMessageType MessageType => MessagesMessageType.File;

        /// <summary>
        /// The file attachment.
        /// Supports supports a wide range of attachments including .zip, .csv and .pdf.
        /// </summary>
        [JsonProperty("file")]
        public CaptionedAttachment File { get; set; }
    }
}