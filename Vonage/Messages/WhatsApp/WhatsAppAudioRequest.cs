using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class WhatsAppAudioRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.WhatsApp;
        public override MessagesMessageType MessageType => MessagesMessageType.Audio;

        /// <summary>
        /// The audio attachment. Supports.aac, .m4a, .amr, .mp3 and.opus
        /// </summary>
        [JsonProperty("audio")]
        public Attachment Audio { get; set; }
    }
}