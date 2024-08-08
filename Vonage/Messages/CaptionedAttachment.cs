#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages;

/// <summary>
/// Represents an attachment to a message.
/// </summary>
public class CaptionedAttachment
{
    /// <summary>
    ///     Additional text to accompany the attachment.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Caption { get; set; }

    /// <summary>
    ///    The publicly accessible URL of the image attachment. The image file is available for 48 hours after it is created. Supported types are .jpg, .jpeg, and .png
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Url { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(2)]
    public string Name { get; set; }
}