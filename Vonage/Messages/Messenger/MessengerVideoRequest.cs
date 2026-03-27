#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
///     Represents a video message request to be sent via Facebook Messenger.
/// </summary>
public class MessengerVideoRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    ///     Messenger-specific settings including message category and tag.
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Video;

    /// <summary>
    ///     The video attachment.
    /// </summary>
    public Attachment Video { get; set; }
}