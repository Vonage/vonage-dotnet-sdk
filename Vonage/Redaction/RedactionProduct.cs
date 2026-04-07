using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vonage.Redaction;

/// <summary>
///     Represents the Vonage product associated with a transaction to be redacted.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum RedactionProduct
{
    /// <summary>
    ///     SMS API transactions. Requires <see cref="RedactionType"/> to be specified.
    /// </summary>
    [EnumMember(Value = "sms")]
    Sms,

    /// <summary>
    ///     Voice API call transactions.
    /// </summary>
    [EnumMember(Value = "voice")]
    Voice,

    /// <summary>
    ///     Number Insight API lookup transactions.
    /// </summary>
    [EnumMember(Value = "number-insight")]
    NumberInsight,

    /// <summary>
    ///     Verify API verification transactions.
    /// </summary>
    [EnumMember(Value = "verify")]
    Verify,

    /// <summary>
    ///     Verify SDK verification transactions.
    /// </summary>
    [EnumMember(Value = "verify-sdk")]
    VerifySdk,

    /// <summary>
    ///     Messages API transactions.
    /// </summary>
    [EnumMember(Value = "messages")]
    Messages
}