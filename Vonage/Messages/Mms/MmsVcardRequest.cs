using Newtonsoft.Json;

namespace Vonage.Messages.Mms
{
    public class MmsVcardRequest : MessageRequestBase
    {
        [JsonProperty("vcard")]
        public Attachment Vcard { get; set; }

        public override MessagesChannel Channel => MessagesChannel.MMS;

        public override MessagesMessageType MessageType => MessagesMessageType.Vcard;
    }
}