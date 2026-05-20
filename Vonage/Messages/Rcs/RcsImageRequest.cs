#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents an image message request to be sent via RCS (Rich Communication Services).
/// </summary>
public class RcsImageRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors().Concat(this.ValidateImage());

    private IEnumerable<string> ValidateImage()
    {
        if (this.Image == null)
            yield return "Image must not be null.";
        else if (string.IsNullOrEmpty(this.Image.Url))
            yield return "Image Url must not be null or empty.";
    }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Image;

    /// <summary>
    ///     The image attachment. Supported types are .jpg, .jpeg, and .png
    /// </summary>
    [JsonPropertyName("image")]
    [JsonPropertyOrder(9)]
    public CaptionedAttachment Image { get; set; }
}