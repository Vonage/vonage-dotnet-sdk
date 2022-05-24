using Newtonsoft.Json;

namespace Vonage.Messages.Mms
{
    public class MmsAudioRequest : MessageRequestBase
    {
        [JsonProperty("audio")]
        public CaptionedAttachment Audio { get; set; }

        public override MessagesChannel Channel => MessagesChannel.MMS;

        public override MessagesMessageType MessageType => MessagesMessageType.Audio;
    }
}