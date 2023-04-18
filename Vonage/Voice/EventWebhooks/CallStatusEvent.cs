using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.EventWebhooks;

public class CallStatusEvent : Event
{
    /// <summary>
    /// The unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    /// Extra detail for the status webhook - only present in some instances
    /// </summary>
    [JsonIgnore]
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

    [JsonProperty("detail")] public string DetailString { get; set; }

    /// <summary>
    /// Call direction, can be either inbound or outbound
    /// </summary>
    [JsonProperty("direction")]
    [JsonConverter(typeof(StringEnumConverter))]
    public Direction Direction { get; set; }

    /// <summary>
    /// The number the call came from
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    /// Call status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// The number the call was made to
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; }
}