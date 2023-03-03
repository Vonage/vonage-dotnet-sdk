using System.Text.Json.Serialization;

namespace Vonage.Messages;

/// <summary>
/// </summary>
public class CaptionedAttachment
{
    /// <summary>
    ///     Additional text to accompany the attachment.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Caption { get; set; }

    /// <summary>
    ///     The URL of the attachment.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Url { get; set; }
}