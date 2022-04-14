using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vonage.Redaction
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedactionType
    {
        [EnumMember(Value = "inbound")]
        Inbound,
        [EnumMember(Value = "outbound")]
        Outbound
    }
}