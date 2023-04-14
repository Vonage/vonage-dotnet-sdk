using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vonage.Redaction;

[JsonConverter(typeof(StringEnumConverter))]
public enum RedactionProduct
{
    [EnumMember(Value = "sms")]
    Sms,
    [EnumMember(Value = "voice")]
    Voice,
    [EnumMember(Value = "number-insight")]
    NumberInsight,
    [EnumMember(Value = "verify")]
    Verify,
    [EnumMember(Value = "verify-sdk")]
    VerifySdk,
    [EnumMember(Value = "messages")]
    Messages
}