#region
using System;
using System.Net;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
/// <example>
///     <code><![CDATA[
/// // Create an HTTP failure for a bad request
/// HttpFailure failure = HttpFailure.From(HttpStatusCode.BadRequest, "Invalid request", "{\"error\":\"missing field\"}");
/// Console.WriteLine(failure.GetFailureMessage()); // "400 - Invalid request - {"error":"missing field"}."
///
/// // Create a simple failure with status code only
/// HttpFailure notFound = HttpFailure.From(HttpStatusCode.NotFound);
/// ]]></code>
/// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// HttpFailure failure = HttpFailure.From(
    ///     HttpStatusCode.Unauthorized,
    ///     "Authentication required",
    ///     "{\"error\":\"token_expired\"}"
    /// );
    /// ]]></code>
    /// </example>
    public static HttpFailure From(HttpStatusCode code, string message, string json) =>
        new HttpFailure(code, message, json);

    /// <summary>
    ///     Creates a HttpFailure.
    /// </summary>
    /// <param name="code">The status code.</param>
    /// <returns>The failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// HttpFailure failure = HttpFailure.From(HttpStatusCode.InternalServerError);
    /// Console.WriteLine(failure.GetFailureMessage()); // "500."
    /// ]]></code>
    /// </example>
    public static HttpFailure From(HttpStatusCode code) => From(code, string.Empty, string.Empty);
}