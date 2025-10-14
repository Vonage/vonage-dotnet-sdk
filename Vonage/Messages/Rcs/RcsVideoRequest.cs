#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsVideoRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Video;

    /// <summary>
    ///     The file attachment. Supports file types .mp4 and .3gpp
    /// </summary>
    [JsonPropertyName("video")]
    [JsonPropertyOrder(9)]
    public CaptionedAttachment Video { get; set; }
}