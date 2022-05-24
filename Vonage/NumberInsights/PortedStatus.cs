using System.Runtime.Serialization;

namespace Vonage.NumberInsights
{
    public enum PortedStatus
    {
        [EnumMember(Value = "unknown")]
        Unknown,
        
        [EnumMember(Value = "ported")]
        Ported,
        
        [EnumMember(Value = "not_ported")]
        NotPorted,
        
        [EnumMember(Value = "assumed_not_ported")]
        AssumedNotPorted,
        
        [EnumMember(Value = "assumed_ported")]
        AssumedPorted
    }
}
