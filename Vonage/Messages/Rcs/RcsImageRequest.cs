#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsImageRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Image;

    /// <summary>
    ///     The duration in seconds the delivery of a message will be attempted. By default Vonage attempts delivery for 72
    ///     hours, however the maximum effective value depends on the operator and is typically 24 - 48 hours. We recommend
    ///     this value should be kept at its default or at least 30 minutes.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(8)]
    public int TimeToLive { get; set; }

    /// <summary>
    ///     The image attachment. Supported types are .jpg, .jpeg, and .png
    /// </summary>
    [JsonPropertyName("image")]
    [JsonPropertyOrder(9)]
    public CaptionedAttachment Image { get; set; }
}