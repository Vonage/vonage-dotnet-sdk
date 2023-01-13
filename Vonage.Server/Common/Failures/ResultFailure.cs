namespace Vonage.Server.Common.Failures;

/// <inheritdoc />
public readonly struct ResultFailure : IResultFailure
{
    private ResultFailure(string error) => this.error = error;
    private readonly string error;

    /// <summary>
    ///     Create a failure from an error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>The failure.</returns>
    public static ResultFailure FromErrorMessage(string error) => new(error);

    /// <inheritdoc />
    public string GetFailureMessage() => this.error;
}