using Newtonsoft.Json;

namespace Vonage.Messages.Mms
{
    public class MmsVideoRequest : MessageRequestBase
    {
        [JsonProperty("video")]
        public CaptionedAttachment Video { get; set; }

        public override MessagesChannel Channel => MessagesChannel.MMS;

        public override MessagesMessageType MessageType => MessagesMessageType.Video;
    }
}