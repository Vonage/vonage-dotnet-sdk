using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Common;

public class Webhook
{
    [JsonProperty("address", Order = 1)] public string Address { get; set; }

    [JsonProperty("http_method", Order = 0)]
    public string Method { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Type
    {
        [EnumMember(Value = "event_url")] EventUrl = 2,

        [EnumMember(Value = "inbound_url")] InboundUrl = 3,

        [EnumMember(Value = "status_url")] StatusUrl = 4,

        [EnumMember(Value = "Unknown")] Unknown = 6,

        [EnumMember(Value = "room_changed")] RoomChanged = 7,

        [EnumMember(Value = "session_changed")]
        SessionChanged = 8,

        [EnumMember(Value = "recording_changed")]
        RecordingChanged = 9,
    }
}