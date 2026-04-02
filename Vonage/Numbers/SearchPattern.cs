namespace Vonage.Numbers;

/// <summary>
///     Specifies the pattern matching strategy when searching for available phone numbers.
/// </summary>
public enum SearchPattern
{
    /// <summary>
    ///     Matches numbers that begin with the specified pattern.
    /// </summary>
    StartsWith = 0,

    /// <summary>
    ///     Matches numbers that contain the specified pattern anywhere in the number.
    /// </summary>
    Contains = 1,

    /// <summary>
    ///     Matches numbers that end with the specified pattern.
    /// </summary>
    EndsWith = 2,
}