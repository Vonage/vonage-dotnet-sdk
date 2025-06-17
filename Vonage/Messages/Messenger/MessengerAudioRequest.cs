#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
/// </summary>
public class MessengerAudioRequest : MessageRequestBase
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("audio")]
    [JsonPropertyOrder(6)]
    public Attachment Audio { get; set; }

    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Audio;
}