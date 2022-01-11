using System.Runtime.Serialization;

namespace Vonage.Voice.Nccos
{
    public enum NccoInputType
    {
        [EnumMember(Value = "dtmf")]
        DTMF = 0,

        [EnumMember(Value = "speech")]
        Speech = 1
    }
}