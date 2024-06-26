using System.Text.Json.Serialization;

namespace Vonage.Messages.Mms;

public class MmsVideoRequest : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.MMS;

    public override MessagesMessageType MessageType => MessagesMessageType.Video;
    [JsonPropertyOrder(6)] public CaptionedAttachment Video { get; set; }

    /// <summary>
    ///     Time-To-Live (how long a message should exist before it is delivered successfully) in seconds. If a message is not
    ///     delivered successfully within the TTL time, the message is considered expired and will be rejected if TTL is
    ///     supported.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(7)]
    public int? TimeToLive { get; set; }
}