#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class Answered : CallStatusEvent
{
    /// <summary>
    /// call start time
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// cost rate for the call
    /// </summary>
    [JsonProperty("rate")]
    [JsonPropertyName("rate")]
    public string Rate { get; set; }

    /// <summary>
    /// Network the call came from
    /// </summary>
    [JsonProperty("network")]
    [JsonPropertyName("network")]
    public string Network { get; set; }
}