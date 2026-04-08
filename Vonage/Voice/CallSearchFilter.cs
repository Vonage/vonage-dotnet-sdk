using System;
using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.Voice;

/// <summary>
///     Represents filter criteria for searching call records via the Get Calls API.
/// </summary>
public class CallSearchFilter
{
    /// <summary>
    ///     Filter by call status. Possible values: "started", "ringing", "answered", "machine", "completed", "busy", "cancelled", "failed", "rejected", "timeout", "unanswered".
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    ///     Return records that occurred after this point in time. Must be in UTC (not converted).
    /// </summary>
    [JsonProperty("date_start")]
    [JsonConverter(typeof(PageListDateTimeConverter))]
    public DateTime? DateStart { get; set; }

    /// <summary>
    ///     Return records that occurred before this point in time. Must be in UTC (not converted).
    /// </summary>
    [JsonProperty("date_end")]
    [JsonConverter(typeof(PageListDateTimeConverter))]
    public DateTime? DateEnd { get; set; }

    /// <summary>
    ///     The number of records to return per page. Range: 1-100. Default is 10.
    /// </summary>
    [JsonProperty("page_size")]
    public int? PageSize { get; set; }

    /// <summary>
    ///     The starting index for pagination. For example, set to 5 with a page size of 10 to return records 50-59. Default is 0.
    /// </summary>
    [JsonProperty("record_index")]
    public int? RecordIndex { get; set; }

    /// <summary>
    ///     The sort order for results: "asc" (ascending, default) or "desc" (descending).
    /// </summary>
    [JsonProperty("order")]
    public string Order { get; set; }

    /// <summary>
    ///     Filter to return only records associated with a specific conversation UUID.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }
}