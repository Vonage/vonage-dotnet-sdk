using System.Text.Json.Serialization;

namespace Vonage.Messages.Mms;

public class MmsAudioRequest : MessageRequestBase
{
    [JsonPropertyOrder(6)] public CaptionedAttachment Audio { get; set; }

    public override MessagesChannel Channel => MessagesChannel.MMS;

    public override MessagesMessageType MessageType => MessagesMessageType.Audio;
}