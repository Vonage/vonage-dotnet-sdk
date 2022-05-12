using Newtonsoft.Json;

namespace Vonage.Messages.Mms
{
    public class MmsImageRequest : MessageRequestBase
    {
        [JsonProperty("image")]
        public Attachment Image { get; set; }

        public override MessagesChannel Channel => MessagesChannel.MMS;

        public override MessagesMessageType MessageType => MessagesMessageType.Image;
    }
}