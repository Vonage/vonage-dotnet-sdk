using System.Net;
using System.Text.Json.Serialization;

namespace Vonage.Common.Failures;

/// <summary>
///     Represents a failure with a HttpStatusCode.
/// </summary>
public readonly struct HttpFailure : IResultFailure
{
    /// <summary>
    ///     The status code.
    /// </summary>
    /// <remarks>Mandatory for deserialization.</remarks>
    public HttpStatusCode Code { get; }

    /// <summary>
    ///     The failure message.
    /// </summary>
    /// <remarks>Mandatory for deserialization.</remarks>
    public string Message { get; }

    /// <summary>
    ///     Create a HttpFailure.
    /// </summary>
    /// <param name="code"> The status code.</param>
    /// <param name="message"> The failure message.</param>
    [JsonConstructor]
    public HttpFailure(HttpStatusCode code, string message)
    {
        this.Code = code;
        this.Message = message;
    }

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

    /// <inheritdoc />
    public string GetFailureMessage() => string.IsNullOrWhiteSpace(this.Message)
        ? $"{(int) this.Code}."
        : $"{(int) this.Code} - {this.Message}.";
}