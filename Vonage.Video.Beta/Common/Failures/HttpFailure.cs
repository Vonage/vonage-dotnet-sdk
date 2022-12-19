using System.Net;

namespace Vonage.Video.Beta.Common.Failures;

/// <summary>
///     Represents a failure with a HttpStatusCode.
/// </summary>
public readonly struct HttpFailure : IResultFailure
{
    private readonly string message;

    private readonly HttpStatusCode code;

    private HttpFailure(HttpStatusCode code, string message)
    {
        this.code = code;
        this.message = message;
    }

    /// <inheritdoc />
    public string GetFailureMessage() => string.IsNullOrWhiteSpace(this.message)
        ? $"{(int) this.code}."
        : $"{(int) this.code} - {this.message}.";

    /// <summary>
    ///     Creates a HttpFailure.
    /// </summary>
    /// <param name="code">The status code.</param>
    /// <param name="message">The message.</param>
    /// <returns>The failure.</returns>
    public static HttpFailure From(HttpStatusCode code, string message) => new(code, message);

    /// <summary>
    ///     Creates a HttpFailure.
    /// </summary>
    /// <param name="code">The status code.</param>
    /// <returns>The failure.</returns>
    public static HttpFailure From(HttpStatusCode code) => From(code, string.Empty);
}