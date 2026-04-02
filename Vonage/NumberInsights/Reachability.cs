using System.Runtime.Serialization;

namespace Vonage.NumberInsights;

/// <summary>
///     Indicates whether a mobile phone number is currently reachable for calls or messages.
/// </summary>
public enum NumberReachability
{
    /// <summary>
    ///     The reachability status could not be determined.
    /// </summary>
    [EnumMember(Value = "unknown")]
    Unknown,

    /// <summary>
    ///     The phone is reachable and can receive calls and messages.
    /// </summary>
    [EnumMember(Value = "reachable")]
    Reachable,

    /// <summary>
    ///     The phone cannot be reached at this time, possibly due to network issues or being out of coverage.
    /// </summary>
    [EnumMember(Value = "undeliverable")]
    Undeliverable,

    /// <summary>
    ///     The phone is not currently connected to the network, such as when powered off or in airplane mode.
    /// </summary>
    [EnumMember(Value = "absent")]
    Absent,

    /// <summary>
    ///     The phone number is invalid or does not exist in the carrier network.
    /// </summary>
    [EnumMember(Value = "bad_number")]
    BadNumber,

    /// <summary>
    ///     The phone number has been blacklisted and cannot receive messages.
    /// </summary>
    [EnumMember(Value = "blacklisted")]
    Blacklisted,
}