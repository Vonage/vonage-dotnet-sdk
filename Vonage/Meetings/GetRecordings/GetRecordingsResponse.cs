using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetRecordings;

/// <summary>
/// </summary>
public struct GetRecordingsResponse
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("_embedded")]
    public EmbeddedResponse Embedded { get; set; }

    /// <summary>
    /// </summary>
    public struct EmbeddedResponse
    {
        /// <summary>
        /// </summary>
        public List<Recording> Recordings { get; set; }
    }
}