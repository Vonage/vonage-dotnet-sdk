using Newtonsoft.Json;

namespace Vonage.Messages.Viber
{
    public class ViberImageRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.ViberService;
        public override MessagesMessageType MessageType => MessagesMessageType.Image;
        
        /// <summary>
        /// The URL of the image attachment.
        /// </summary>
        [JsonProperty("image")]
        public Attachment Image { get; set; }
        
        [JsonProperty("viber_service")]
        public ViberRequestData Data { get; set; }
    }
}