using System.Text.Json.Serialization;

namespace Vonage.Messages.Mms;

public class MmsImageRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.MMS;
    [JsonPropertyOrder(6)] public Attachment Image { get; set; }

    public override MessagesMessageType MessageType => MessagesMessageType.Image;

    /// <summary>
    ///     Time-To-Live (how long a message should exist before it is delivered successfully) in seconds. If a message is not
    ///     delivered successfully within the TTL time, the message is considered expired and will be rejected if TTL is
    ///     supported.
    /// </summary>
    [JsonPropertyOrder(7)]
    [JsonPropertyName("ttl")]
    public int? TimeToLive { get; set; }
}