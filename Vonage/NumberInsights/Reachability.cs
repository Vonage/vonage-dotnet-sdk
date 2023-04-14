using System.Runtime.Serialization;

namespace Vonage.NumberInsights;

public enum NumberReachability
{
    [EnumMember(Value = "unknown")]
    Unknown,
        
    [EnumMember(Value = "reachable")]
    Reachable,
        
    [EnumMember(Value = "undeliverable")]
    Undeliverable,
        
    [EnumMember(Value = "absent")]
    Absent,
        
    [EnumMember(Value = "bad_number")]
    BadNumber,
        
    [EnumMember(Value = "blacklisted")]
    Blacklisted
}