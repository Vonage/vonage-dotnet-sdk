#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
///     Represents a file message request to be sent via Facebook Messenger.
/// </summary>
public class MessengerFileRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    ///     Messenger-specific settings including message category and tag.
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <summary>
    ///     The file attachment.
    /// </summary>
    [JsonPropertyOrder(6)]
    public Attachment File { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.File;
}