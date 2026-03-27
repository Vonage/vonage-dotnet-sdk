#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages;

/// <summary>
///     Represents an attachment (image, audio, video, or file) for a message.
/// </summary>
public class Attachment
{
    /// <summary>
    ///     The publicly accessible URL of the attachment.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Url { get; set; }

    /// <summary>
    ///     Additional text to accompany the attachment.
    /// </summary>
    [JsonPropertyOrder(2)]
    public string Caption { get; set; }

    /// <summary>
    ///     The MIME type of the attachment (e.g., "image/jpeg", "video/mp4"). Optional.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(0)]
    public string Type { get; set; }
}