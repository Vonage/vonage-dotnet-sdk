using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.NumberInsights;

[JsonConverter(typeof(StringEnumConverter))]
public enum RoamingStatus
{
    [EnumMember(Value = "unknown")]
    Unknown,
        
    [EnumMember(Value = "roaming")]
    Roaming,
        
    [EnumMember(Value = "not_roaming")]
    NotRoaming
}