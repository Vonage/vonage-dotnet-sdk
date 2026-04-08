#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Base class for Voice API call status webhook events. Provides common properties such as direction, from/to numbers, conversation UUID, and detailed status information.
/// </summary>
public class CallStatusEvent : Event
{
    /// <summary>
    ///     The unique identifier for the conversation this call leg belongs to.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     Extra detail for the status webhook, parsed as a <see cref="DetailedStatus"/> enum. Returns <see cref="DetailedStatus.no_detail"/> if no detail is present, or <see cref="DetailedStatus.unmapped_detail"/> if the value cannot be mapped.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public DetailedStatus Detail =>
        string.IsNullOrEmpty(this.DetailString)
            ? DetailedStatus.no_detail
            : Enum.TryParse(this.DetailString, out DetailedStatus detail)
                ? detail
                : DetailedStatus.unmapped_detail;

    /// <summary>
    ///     The raw detail string from the webhook payload. Use <see cref="Detail"/> for the parsed enum value.
    /// </summary>
    [JsonProperty("detail")]
    [JsonPropertyName("detail")]
    public string DetailString { get; set; }

    /// <summary>
    ///     The call direction: <see cref="Direction.inbound"/> or <see cref="Direction.outbound"/>.
    /// </summary>
    [JsonProperty("direction")]
    [JsonPropertyName("direction")]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter<Direction>))]
    public Direction Direction { get; set; }

    /// <summary>
    ///     The phone number or SIP URI the call originated from.
    /// </summary>
    [JsonProperty("from")]
    [JsonPropertyName("from")]
    public string From { get; set; }

    /// <summary>
    ///     The current status of the call (e.g. started, ringing, answered, completed, failed, busy, cancelled, rejected, timeout, unanswered).
    /// </summary>
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    ///     The phone number or SIP URI the call was made to.
    /// </summary>
    [JsonProperty("to")]
    [JsonPropertyName("to")]
    public string To { get; set; }
}