using System;
using System.Net;
using System.Text.Json.Serialization;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <inheritdoc />
public readonly struct HttpFailure : IResultFailure
{
    /// <summary>
    ///     The status code.
    /// </summary>
    /// <remarks>Mandatory for deserialization.</remarks>
    public HttpStatusCode Code { get; }

    /// <summary>
    ///     The JSON content.
    /// </summary>
    public string Json { get; }

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
    /// <param name="json">The JSON content.</param>
    [JsonConstructor]
    public HttpFailure(HttpStatusCode code, string message, string json)
    {
        this.Code = code;
        this.Message = message;
        this.Json = json;
    }

    /// <summary>
    ///     Creates a HttpFailure.
    /// </summary>
    /// <param name="code">The status code.</param>
    /// <param name="message">The message.</param>
    /// <param name="json">The JSON content.</param>
    /// <returns>The failure.</returns>
    public static HttpFailure From(HttpStatusCode code, string message, string json) => new(code, message, json);

    /// <summary>
    ///     Creates a HttpFailure.
    /// </summary>
    /// <param name="code">The status code.</param>
    /// <returns>The failure.</returns>
    public static HttpFailure From(HttpStatusCode code) => From(code, string.Empty, string.Empty);

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
}