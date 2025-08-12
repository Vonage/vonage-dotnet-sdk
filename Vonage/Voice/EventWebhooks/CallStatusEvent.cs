#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class CallStatusEvent : Event
{
    /// <summary>
    /// The unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    /// Extra detail for the status webhook - only present in some instances
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public DetailedStatus Detail
    {
        get
        {
            DetailedStatus detail;
            if (string.IsNullOrEmpty(this.DetailString))
            {
                return DetailedStatus.no_detail;
            }

            if (Enum.TryParse(this.DetailString, out detail))
            {
                return detail;
            }

            return DetailedStatus.unmapped_detail;
        }
    }

    [JsonProperty("detail")]
    [JsonPropertyName("detail")]
    public string DetailString { get; set; }

    /// <summary>
    /// Call direction, can be either inbound or outbound
    /// </summary>
    [JsonProperty("direction")]
    [JsonPropertyName("direction")]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter<Direction>))]
    public Direction Direction { get; set; }

    /// <summary>
    /// The number the call came from
    /// </summary>
    [JsonProperty("from")]
    [JsonPropertyName("from")]
    public string From { get; set; }

    /// <summary>
    /// Call status
    /// </summary>
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    /// The number the call was made to
    /// </summary>
    [JsonProperty("to")]
    [JsonPropertyName("to")]
    public string To { get; set; }
}