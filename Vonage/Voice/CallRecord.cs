using System;
using Newtonsoft.Json;

namespace Vonage.Voice;

/// <summary>
///     Represents the detailed record of a voice call, including status, timing, pricing, and endpoint information.
/// </summary>
public class CallRecord
{
    /// <summary>
    ///     The unique identifier for the conversation this call is part of.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     The direction of the call: "outbound" or "inbound".
    /// </summary>
    [JsonProperty("direction")]
    public string Direction { get; set; }

    /// <summary>
    ///     The time elapsed for the call in seconds. Only present when status is "completed".
    /// </summary>
    [JsonProperty("duration")]
    public string Duration { get; set; }

    /// <summary>
    ///     The time the call ended in the format YYYY-MM-DD HH:MM:SS. Only present when status is "completed".
    /// </summary>
    [JsonProperty("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    ///     The endpoint the call originated from.
    /// </summary>
    [JsonProperty("from")]
    public CallEndpoint From { get; set; }

    /// <summary>
    ///     HAL links for navigating to this call resource.
    /// </summary>
    [JsonProperty("_links")] public Common.HALLinks Links { get; set; }

    /// <summary>
    ///     The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier network used to make this call. Only present when status is "completed".
    /// </summary>
    [JsonProperty("network")]
    public string Network { get; set; }

    /// <summary>
    ///     The total price charged for this call. Only present when status is "completed".
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    ///     The price per minute for this call. Only present when status is "completed".
    /// </summary>
    [JsonProperty("rate")]
    public string Rate { get; set; }

    /// <summary>
    ///     The URL to download a call or conversation recording from.
    /// </summary>
    [JsonProperty("recording_url")]
    public string RecordingUrl { get; set; }

    /// <summary>
    ///     The time the call started in the format YYYY-MM-DD HH:MM:SS. Only present when status is "completed".
    /// </summary>
    [JsonProperty("start_time")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    ///     The status of the call: started, ringing, answered, machine, timeout, completed, busy, cancelled, failed, rejected, or unanswered.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    ///     The destination endpoint the call was connected to (phone, SIP, WebSocket, or VBC extension).
    /// </summary>
    [JsonProperty("to")]
    public CallEndpoint To { get; set; }

    /// <summary>
    ///     The unique identifier (UUID) for this call leg.
    /// </summary>
    [JsonProperty("uuid")]
    public string Uuid { get; set; }
}