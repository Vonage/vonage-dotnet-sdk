using System.Text.Json.Serialization;

namespace Vonage.Meetings.Common;

/// <summary>
/// </summary>
public struct RoomLinks
{
    /// <summary>
    /// </summary>
    public Link First { get; set; }

    /// <summary>
    /// </summary>
    public Link Next { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("prev")]
    public Link Previous { get; set; }

    /// <summary>
    /// </summary>
    public Link Self { get; set; }
}