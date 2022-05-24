using Newtonsoft.Json;

namespace Vonage.Messages.Sms
{
    public class SmsRequest : MessageRequestBase
    {
        public override MessagesChannel Channel => MessagesChannel.SMS;
        
        public override MessagesMessageType MessageType => MessagesMessageType.Text;

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}