using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

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
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RecordingStatus>))]
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