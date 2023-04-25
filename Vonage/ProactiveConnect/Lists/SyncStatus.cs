using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.ProactiveConnect.Lists;

/// <summary>
///     Synchronization status between the list content (items) and it's datasource.
/// </summary>
public struct SyncStatus
{
    /// <summary>
    ///     One or more list items were added, removed and/or modified since latest sync.
    /// </summary>
    [JsonPropertyName("data_modified")]
    public bool DataModified { get; set; }

    /// <summary>
    ///     Provide details on sync status.
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    ///     Indicates if the list content or metadata were modified since last sync.
    /// </summary>
    public bool Dirty { get; set; }

    /// <summary>
    ///     List metadata (definition) is modified since latest sync.
    /// </summary>
    [JsonPropertyName("metadata_modified")]
    public bool MetadataModified { get; set; }

    /// <summary>
    ///     Sync status.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<Status>))]
    public Status Value { get; set; }

    /// <summary>
    ///     Represents the different statuses of a synchronization.
    /// </summary>
    public enum Status
    {
        /// <summary>
        ///     The "configured" status.
        /// </summary>
        [Description("configured")] Configured,

        /// <summary>
        ///     The "clearing" status.
        /// </summary>
        [Description("clearing")] Clearing,

        /// <summary>
        ///     The "fetching" status.
        /// </summary>
        [Description("fetching")] Fetching,

        /// <summary>
        ///     The "paused" status.
        /// </summary>
        [Description("paused")] Paused,

        /// <summary>
        ///     The "cancelled" status.
        /// </summary>
        [Description("cancelled")] Cancelled,

        /// <summary>
        ///     The "completed" status.
        /// </summary>
        [Description("completed")] Completed,

        /// <summary>
        ///     The "failed" status.
        /// </summary>
        [Description("failed")] Failed,
    }
}