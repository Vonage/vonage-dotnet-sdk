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
    /// <param name="endedAt"></param>
    /// <param name="id"></param>
    /// <param name="sessionId"></param>
    /// <param name="startedAt"></param>
    /// <param name="status"></param>
    /// <param name="links"></param>
    public Recording(string endedAt, string id, string sessionId, string startedAt, RecordingStatus status,
        RecordingLinks links)
    {
        this.EndedAt = endedAt;
        this.Id = id;
        this.SessionId = sessionId;
        this.StartedAt = startedAt;
        this.Status = status;
        this.Links = links;
    }

    /// <summary>
    /// </summary>
    public struct RecordingLinks
    {
        /// <summary>
        /// </summary>
        public Link Url { get; set; }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="url"></param>
        public RecordingLinks(Link url)
        {
            this.Url = url;
        }
    }
}