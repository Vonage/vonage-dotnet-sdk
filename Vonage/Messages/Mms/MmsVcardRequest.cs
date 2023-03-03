using System.Text.Json.Serialization;

namespace Vonage.Messages.Mms;

public class MmsVcardRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.MMS;

    public override MessagesMessageType MessageType => MessagesMessageType.Vcard;
    [JsonPropertyOrder(6)] public Attachment Vcard { get; set; }
}