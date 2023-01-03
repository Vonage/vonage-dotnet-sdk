using System.Net;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Represents an error api response.
/// </summary>
public readonly struct ErrorResponse
{
    /// <summary>
    ///     The response code.
    /// </summary>
    public HttpStatusCode Code { get; }

    /// <summary>
    ///     The response message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    ///     Creates a response.
    /// </summary>
    /// <param name="code">The response code.</param>
    /// <param name="message">The response message.</param>
    public ErrorResponse(HttpStatusCode code, string message)
    {
        this.Code = code;
        this.Message = message;
    }

    /// <summary>
    ///     Converts to HttpFailure.
    /// </summary>
    /// <returns>The failure.</returns>
    public HttpFailure ToHttpFailure() => HttpFailure.From(this.Code, this.Message ?? string.Empty);
}