using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.NumberInsights;

/// <summary>
///     Indicates whether a mobile phone number is currently roaming outside its home network.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum RoamingStatus
{
    /// <summary>
    ///     The roaming status could not be determined.
    /// </summary>
    [EnumMember(Value = "unknown")]
    Unknown,

    /// <summary>
    ///     The phone number is currently roaming on a network outside its home country.
    /// </summary>
    [EnumMember(Value = "roaming")]
    Roaming,

    /// <summary>
    ///     The phone number is not roaming and is connected to its home network.
    /// </summary>
    [EnumMember(Value = "not_roaming")]
    NotRoaming,
}