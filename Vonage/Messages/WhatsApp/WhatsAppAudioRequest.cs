#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send an audio message on WhatsApp.
/// </summary>
public class WhatsAppAudioRequest : WhatsAppMessageBase
{
    /// <summary>
    ///     The audio attachment. Supports.aac, .m4a, .amr, .mp3 and.opus
    /// </summary>
    [JsonPropertyOrder(9)]
    public Attachment Audio { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Audio;
}