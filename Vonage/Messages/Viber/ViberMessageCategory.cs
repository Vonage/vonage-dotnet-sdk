using System.Runtime.Serialization;

namespace Vonage.Messages.Viber
{
    public enum ViberMessageCategory
    {
        [EnumMember(Value = "transaction")]
        Transaction = 0,
        
        [EnumMember(Value = "promotion")]
        Promotion = 1
    }
}