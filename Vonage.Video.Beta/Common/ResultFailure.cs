namespace Vonage.Video.Beta.Common;

/// <inheritdoc />
public readonly struct ResultFailure : IResultFailure
{
    private readonly string error;
    private ResultFailure(string error) => this.error = error;

    /// <summary>
    ///     Create a failure from an error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>The failure.</returns>
    public static ResultFailure FromErrorMessage(string error) => new(error);

    /// <inheritdoc />
    public string GetFailureMessage() => this.error;
}