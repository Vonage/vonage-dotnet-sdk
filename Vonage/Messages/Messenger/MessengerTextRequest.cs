#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Messenger;

/// <summary>
/// </summary>
public class MessengerTextRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.Messenger;

    /// <summary>
    /// </summary>
    [JsonPropertyName("messenger")]
    public MessengerRequestData Data { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Text;

    /// <summary>
    ///     The text of message to send; limited to 640 characters, including unicode.
    /// </summary>
    public string Text { get; set; }
}