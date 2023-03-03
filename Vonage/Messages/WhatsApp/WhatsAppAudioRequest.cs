using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppAudioRequest : MessageRequestBase
{
    /// <summary>
    ///     The audio attachment. Supports.aac, .m4a, .amr, .mp3 and.opus
    /// </summary>
    [JsonPropertyOrder(6)]
    public Attachment Audio { get; set; }

    public override MessagesChannel Channel => MessagesChannel.WhatsApp;
    public override MessagesMessageType MessageType => MessagesMessageType.Audio;
}