#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Mms;

/// <summary>
///     Represents an MMS message request with multiple content attachments.
/// </summary>
public class MmsContentRequest : MmsMessageBase
{
    /// <summary>
    ///     The array of attachments to include in the message.
    /// </summary>
    [JsonPropertyOrder(8)]
    public Attachment[] Content { get; set; }

    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.MMS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Content;

    /// <summary>
    ///     Time-To-Live (how long a message should exist before it is delivered successfully) in seconds. If a message is not
    ///     delivered successfully within the TTL time, the message is considered expired and will be rejected if TTL is
    ///     supported.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(9)]
    public int? TimeToLive { get; set; }
}