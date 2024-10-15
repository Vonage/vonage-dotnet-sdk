#region
using System;
using System.Net;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
public record HttpFailure(HttpStatusCode Code, string Message, string Json) : IResultFailure
{
    /// <inheritdoc />
    public Type Type => typeof(HttpFailure);

    /// <inheritdoc />
    public string GetFailureMessage() => string.IsNullOrWhiteSpace(this.Message)
        ? $"{(int) this.Code}."
        : $"{(int) this.Code} - {this.Message} - {this.Json}.";

    /// <inheritdoc />
    public Exception ToException() => new VonageHttpRequestException(this.Message)
    {
        HttpStatusCode = this.Code,
        Json = this.Json,
    };

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);

    /// <summary>
    ///     Creates a HttpFailure.
    /// </summary>
    /// <param name="code">The status code.</param>
    /// <param name="message">The message.</param>
    /// <param name="json">The JSON content.</param>
    /// <returns>The failure.</returns>
    public static HttpFailure From(HttpStatusCode code, string message, string json) =>
        new HttpFailure(code, message, json);

    /// <summary>
    ///     Creates a HttpFailure.
    /// </summary>
    /// <param name="code">The status code.</param>
    /// <returns>The failure.</returns>
    public static HttpFailure From(HttpStatusCode code) => From(code, string.Empty, string.Empty);
}