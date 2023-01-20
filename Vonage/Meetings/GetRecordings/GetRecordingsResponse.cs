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
    ///     Constructor.
    /// </summary>
    /// <param name="embedded"></param>
    public GetRecordingsResponse(EmbeddedResponse embedded) => this.Embedded = embedded;

    /// <summary>
    /// </summary>
    public struct EmbeddedResponse
    {
        /// <summary>
        /// </summary>
        public List<Recording> Recordings { get; set; }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="recordings"></param>
        public EmbeddedResponse(List<Recording> recordings) => this.Recordings = recordings;
    }
}