using System.Runtime.Serialization;

namespace Vonage.NumberInsights;

/// <summary>
///     Indicates whether a phone number has been ported from one carrier to another.
/// </summary>
public enum PortedStatus
{
    /// <summary>
    ///     The porting status could not be determined.
    /// </summary>
    [EnumMember(Value = "unknown")]
    Unknown,

    /// <summary>
    ///     The phone number has been ported to a different carrier than originally assigned.
    /// </summary>
    [EnumMember(Value = "ported")]
    Ported,

    /// <summary>
    ///     The phone number remains with its original carrier and has not been ported.
    /// </summary>
    [EnumMember(Value = "not_ported")]
    NotPorted,

    /// <summary>
    ///     The carrier has not explicitly confirmed porting status, but the number is assumed to be with the original carrier.
    /// </summary>
    [EnumMember(Value = "assumed_not_ported")]
    AssumedNotPorted,

    /// <summary>
    ///     The carrier has not explicitly confirmed porting status, but the number is assumed to have been ported.
    /// </summary>
    [EnumMember(Value = "assumed_ported")]
    AssumedPorted,
}