using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send an audio message on WhatsApp.
/// </summary>
public struct WhatsAppAudioRequest : IWhatsAppMessage
{
    /// <summary>
    ///     The audio attachment. Supports.aac, .m4a, .amr, .mp3 and.opus
    /// </summary>
    [JsonPropertyOrder(5)]
    public Attachment Audio { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Audio;

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }
}