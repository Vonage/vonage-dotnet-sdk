#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Mms;

/// <summary>
/// </summary>
public class MmsImageRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.MMS;

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(8)]
    public Attachment Image { get; set; }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Image;

    /// <summary>
    ///     Time-To-Live (how long a message should exist before it is delivered successfully) in seconds. If a message is not
    ///     delivered successfully within the TTL time, the message is considered expired and will be rejected if TTL is
    ///     supported.
    /// </summary>
    [JsonPropertyOrder(9)]
    [JsonPropertyName("ttl")]
    public int? TimeToLive { get; set; }
}