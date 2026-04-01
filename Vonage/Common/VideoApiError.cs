#region
using System.Net;
using Vonage.Common.Failures;
#endregion

namespace Vonage.Common;

/// <summary>
///     Defines the contract for API error responses that can be converted to <see cref="HttpFailure" /> instances.
/// </summary>
internal interface IApiError
{
    /// <summary>
    ///     Converts the API error to an <see cref="HttpFailure" /> for consistent error handling.
    /// </summary>
    /// <returns>An <see cref="HttpFailure" /> representing the API error.</returns>
    HttpFailure ToFailure();
}

/// <summary>
///     Represents an error response from the Vonage Video API.
/// </summary>
/// <param name="Code">The HTTP status code of the error response.</param>
/// <param name="Message">The error message describing what went wrong.</param>
internal record VideoApiError(HttpStatusCode Code, string Message) : IApiError
{
    /// <inheritdoc />
    public HttpFailure ToFailure() => HttpFailure.From(this.Code, this.Message, null);
}

/// <summary>
///     Represents a standard RFC 7807 Problem Details error response from Vonage APIs.
/// </summary>
/// <param name="Type">A URI reference identifying the problem type.</param>
/// <param name="Title">A short, human-readable summary of the problem.</param>
/// <param name="Detail">A human-readable explanation specific to this occurrence of the problem.</param>
/// <param name="Instance">A URI reference identifying the specific occurrence of the problem.</param>
internal record StandardApiError(string Type, string Title, string Detail, string Instance) : IApiError
{
    /// <inheritdoc />
    public HttpFailure ToFailure() => HttpFailure.From(HttpStatusCode.Accepted, this.Title, null);
}

/// <summary>
///     Represents an error response from Vonage Network APIs.
/// </summary>
/// <param name="Status">The HTTP status code of the error.</param>
/// <param name="Code">The error code identifying the type of error.</param>
/// <param name="Message">The error message describing what went wrong.</param>
internal record NetworkApiError(int Status, string Code, string Message) : IApiError
{
    /// <inheritdoc />
    public HttpFailure ToFailure() => HttpFailure.From(HttpStatusCode.Accepted, this.Message, null);
}