#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
///     Represents an audio message request to be sent via Facebook Messenger.
/// </summary>
public class MessengerAudioRequest : MessageRequestBase
{
    /// <summary>
    ///     The audio attachment.
    /// </summary>
    [JsonPropertyName("audio")]
    [JsonPropertyOrder(6)]
    public Attachment Audio { get; set; }

    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    ///     Messenger-specific settings including message category and tag.
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Audio;
}