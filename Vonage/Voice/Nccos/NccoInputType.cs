using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.Nccos;

[JsonConverter(typeof(StringEnumConverter))]
public enum NccoInputType
{
    [EnumMember(Value = "dtmf")]
    DTMF = 0,

    [EnumMember(Value = "speech")]
    Speech = 1
}