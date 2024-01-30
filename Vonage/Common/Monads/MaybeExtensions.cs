namespace Vonage.Common.Monads;

/// <summary>
///     Exposes a set of extensions for Maybe.
/// </summary>
public static class MaybeExtensions
{
    /// <summary>
    ///     Creates a Maybe from a string value.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A None state if the value is null or whitespace. A Some state otherwise.</returns>
    public static Maybe<string> From(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? Maybe<string>.None
            : Maybe<string>.Some(value);
}