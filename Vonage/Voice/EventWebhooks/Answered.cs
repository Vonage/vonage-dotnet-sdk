#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a call is answered by the recipient. Contains the start time, cost rate, and carrier network information.
/// </summary>
public class Answered : CallStatusEvent
{
    /// <summary>
    ///     The timestamp when the call was answered, in ISO 8601 format.
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    ///     The price per minute for this call in EUR.
    /// </summary>
    [JsonProperty("rate")]
    [JsonPropertyName("rate")]
    public string Rate { get; set; }

    /// <summary>
    ///     The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier network used to make this call.
    /// </summary>
    [JsonProperty("network")]
    [JsonPropertyName("network")]
    public string Network { get; set; }
}