#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents a video message request to be sent via RCS (Rich Communication Services).
/// </summary>
public class RcsVideoRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors().Concat(this.ValidateVideo());

    private IEnumerable<string> ValidateVideo()
    {
        if (this.Video == null)
            yield return "Video must not be null.";
        else if (string.IsNullOrEmpty(this.Video.Url))
            yield return "Video Url must not be null or empty.";
    }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Video;

    /// <summary>
    ///     The file attachment. Supports file types .mp4 and .3gpp
    /// </summary>
    [JsonPropertyName("video")]
    [JsonPropertyOrder(9)]
    public CaptionedAttachment Video { get; set; }
}