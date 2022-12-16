namespace Vonage.Video.Beta.Common;

/// <summary>
///     Represents a Failure with an error message.
/// </summary>
public readonly struct ResultFailure
{
    /// <summary>
    ///     The error message.
    /// </summary>
    public string Error { get; }

    private ResultFailure(string error) => this.Error = error;

    /// <summary>
    ///     Create a failure from an error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>The failure.</returns>
    public static ResultFailure FromErrorMessage(string error) => new(error);
}