#region
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
    public override MessagesMessageType MessageType => MessagesMessageType.Image;

    /// <summary>
    ///     The image attachment. Supported types are .jpg, .jpeg, and .png
    /// </summary>
    [JsonPropertyName("image")]
    [JsonPropertyOrder(9)]
    public CaptionedAttachment Image { get; set; }
}