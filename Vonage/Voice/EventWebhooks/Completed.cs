#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class Completed : CallStatusEvent
{
    /// <summary>
    ///     Value from CS sip:hangup event sent to Voice API.
    /// </summary>
    [JsonProperty("sip_code")]
    [JsonPropertyName("sip_code")]
    public int? SipCode { get; set; }

    /// <summary>
    /// Call length (in seconds)
    /// </summary>
    [JsonProperty("duration")]
    [JsonPropertyName("duration")]
    public string Duration { get; set; }

    /// <summary>
    /// Timestamp (ISO 8601 format) of the end time of the call
    /// </summary>
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// The type of network that was used in the call
    /// </summary>
    [JsonProperty("network")]
    [JsonPropertyName("network")]
    public string Network { get; set; }

    /// <summary>
    /// Total cost of the call (EUR)
    /// </summary>
    [JsonProperty("price")]
    [JsonPropertyName("price")]
    public string Price { get; set; }

    /// <summary>
    /// Cost per minute of the call (EUR)
    /// </summary>
    [JsonProperty("rate")]
    [JsonPropertyName("rate")]
    public string Rate { get; set; }

    /// <summary>
    /// Timestamp (ISO 8601 format)
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; set; }
}