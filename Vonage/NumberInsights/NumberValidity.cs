namespace Vonage.NumberInsights;

/// <summary>
///     Indicates whether a phone number is valid and exists in the carrier network.
/// </summary>
public enum NumberValidity
{
    /// <summary>
    ///     The validity of the number could not be determined.
    /// </summary>
    unknown,

    /// <summary>
    ///     The number is valid and registered with a carrier.
    /// </summary>
    valid,

    /// <summary>
    ///     The number is not valid or does not exist.
    /// </summary>
    not_valid,

    /// <summary>
    ///     The number could not be definitively validated but is assumed to be valid based on available information.
    /// </summary>
    inferred,

    /// <summary>
    ///     The number could not be definitively validated but is assumed to be invalid based on available information.
    /// </summary>
    inferred_not_valid,
}