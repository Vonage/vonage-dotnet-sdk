using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vonage.Redaction;

/// <summary>
///     Represents the direction of a transaction for redaction purposes.
/// </summary>
/// <remarks>
///     This type is required when redacting SMS data to specify whether the message was sent to or received from the user.
/// </remarks>
[JsonConverter(typeof(StringEnumConverter))]
public enum RedactionType
{
    /// <summary>
    ///     The transaction was inbound (received from a user).
    /// </summary>
    [EnumMember(Value = "inbound")]
    Inbound,

    /// <summary>
    ///     The transaction was outbound (sent to a user).
    /// </summary>
    [EnumMember(Value = "outbound")]
    Outbound
}