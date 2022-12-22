using System.Net;

namespace Vonage.Video.Beta.Video.Sessions;

/// <summary>
///     Represents an error api response.
/// </summary>
public struct ErrorResponse
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
}