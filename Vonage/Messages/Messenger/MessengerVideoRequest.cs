#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
/// </summary>
public class MessengerVideoRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Video;

    /// <summary>
    /// </summary>
    public Attachment Video { get; set; }
}