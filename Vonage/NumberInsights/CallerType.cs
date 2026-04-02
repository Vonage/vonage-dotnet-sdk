namespace Vonage.NumberInsights;

/// <summary>
///     Indicates whether the phone number owner is a business or individual consumer.
///     Only available for US numbers when CNAM lookup is enabled.
/// </summary>
public enum CallerType
{
    /// <summary>
    ///     The caller type could not be determined.
    /// </summary>
    unknown,

    /// <summary>
    ///     The phone number is registered to a business entity.
    /// </summary>
    business,

    /// <summary>
    ///     The phone number is registered to an individual consumer.
    /// </summary>
    consumer,
}