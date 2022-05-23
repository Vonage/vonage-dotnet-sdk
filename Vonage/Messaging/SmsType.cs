using System.Runtime.Serialization;

namespace Vonage.Messaging
{
    public enum SmsType
    {
        [EnumMember(Value = "text")]
        Text = 1,
        
        [EnumMember(Value = "binary")]
        Binary = 2,
        
        [EnumMember(Value = "wappush")]
        Wappush = 3,
        
        [EnumMember(Value = "unicode")]
        Unicode = 4,
        
        [EnumMember(Value = "vcal")]
        VCal = 5,
        
        [EnumMember(Value = "vcar")]
        VCar = 6
    }
}
