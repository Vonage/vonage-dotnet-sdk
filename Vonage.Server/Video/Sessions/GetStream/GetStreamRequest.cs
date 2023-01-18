using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Sessions.GetStream;

/// <summary>
///     Represents a request to retrieve a stream.
/// </summary>
public readonly struct GetStreamRequest : IVonageRequest
{
    private GetStreamRequest(string applicationId, string sessionId, string streamId)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.StreamId = streamId;
    }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The session Id.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     The stream Id.
    /// </summary>
    public string StreamId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream/{this.StreamId}";

    /// <summary>
    ///     Parses the input into a GetStreamRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="sessionId">The session Id.</param>
    /// <param name="streamId">The stream Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetStreamRequest> Parse(string applicationId, string sessionId, string streamId) =>
        Result<GetStreamRequest>
            .FromSuccess(new GetStreamRequest(applicationId, sessionId, streamId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyStreamId)
            .Bind(VerifySessionId);

    private static Result<GetStreamRequest> VerifyApplicationId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<GetStreamRequest> VerifySessionId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    private static Result<GetStreamRequest> VerifyStreamId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}