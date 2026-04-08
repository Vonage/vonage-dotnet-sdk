#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a call has ended normally. Contains billing details (price, rate, duration), network information, and timing data.
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
    ///     The duration of the call in seconds.
    /// </summary>
    [JsonProperty("duration")]
    [JsonPropertyName("duration")]
    public string Duration { get; set; }

    /// <summary>
    ///     The timestamp when the call ended, in ISO 8601 format.
    /// </summary>
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    ///     The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier network used to make this call.
    /// </summary>
    [JsonProperty("network")]
    [JsonPropertyName("network")]
    public string Network { get; set; }

    /// <summary>
    ///     The total price charged for this call in EUR.
    /// </summary>
    [JsonProperty("price")]
    [JsonPropertyName("price")]
    public string Price { get; set; }

    /// <summary>
    ///     The price per minute for this call in EUR.
    /// </summary>
    [JsonProperty("rate")]
    [JsonPropertyName("rate")]
    public string Rate { get; set; }

    /// <summary>
    ///     The timestamp when the call started, in ISO 8601 format.
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// Indicates who terminated the call. Can be <c>platform</c> (the call was terminated by the Voice API platform,
    /// for example the NCCO finished its last action) or <c>user</c> (the call was terminated by the user,
    /// for example the user hung up, rejected, or didn't answer).
    /// </summary>
    [JsonProperty("disconnected_by")]
    [JsonPropertyName("disconnected_by")]
    public string DisconnectedBy { get; set; }
}