using System.Text.Json.Serialization;

namespace Vonage.Meetings.Common;

/// <summary>
/// </summary>
public struct Recording
{
    /// <summary>
    /// </summary>
    public string EndedAt { get; set; }

    /// <summary>
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("_links")]
    public RecordingLinks Links { get; set; }

    /// <summary>
    /// </summary>
    public string SessionId { get; set; }

    /// <summary>
    /// </summary>
    public string StartedAt { get; set; }

    /// <summary>
    /// </summary>
    public RecordingStatus Status { get; set; }

    /// <summary>
    /// </summary>
    public struct RecordingLinks
    {
        /// <summary>
        /// </summary>
        public Link Url { get; set; }
    }
}