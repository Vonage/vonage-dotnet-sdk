#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Mms;

/// <summary>
///     Represents an MMS audio message request.
/// </summary>
public class MmsAudioRequest : MmsMessageBase
{
    /// <summary>
    ///     The audio attachment.
    /// </summary>
    [JsonPropertyOrder(8)]
    public CaptionedAttachment Audio { get; set; }

    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.MMS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Audio;

    /// <summary>
    ///     Time-To-Live (how long a message should exist before it is delivered successfully) in seconds. If a message is not
    ///     delivered successfully within the TTL time, the message is considered expired and will be rejected if TTL is
    ///     supported.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(9)]
    public int? TimeToLive { get; set; }
}