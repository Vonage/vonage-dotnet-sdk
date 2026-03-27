#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
///     Represents an image message request to be sent via Facebook Messenger.
/// </summary>
public class MessengerImageRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    ///     Messenger-specific settings including message category and tag.
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <summary>
    ///     The image attachment.
    /// </summary>
    [JsonPropertyOrder(6)]
    public Attachment Image { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Image;
}