#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
/// </summary>
public class MessengerFileRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(6)]
    public Attachment File { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.File;
}