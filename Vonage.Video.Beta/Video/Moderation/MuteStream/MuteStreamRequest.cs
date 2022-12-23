using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Moderation.MuteStream;

/// <summary>
///     Represents a request to mute a stream.
/// </summary>
public readonly struct MuteStreamRequest : IVideoRequest
{
    private MuteStreamRequest(string applicationId, string sessionId, string streamId)
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

    /// <summary>
    ///     Parses the input into a MuteStreamRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="sessionId">The session Id.</param>
    /// <param name="streamId">The stream Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<MuteStreamRequest> Parse(string applicationId, string sessionId, string streamId) =>
        Result<MuteStreamRequest>
            .FromSuccess(new MuteStreamRequest(applicationId, sessionId, streamId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyStreamId)
            .Bind(VerifySessionId);

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/project/{this.ApplicationId}/session/{this.SessionId}/stream/{this.StreamId}/mute";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    private static Result<MuteStreamRequest> VerifyApplicationId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<MuteStreamRequest> VerifySessionId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    private static Result<MuteStreamRequest> VerifyStreamId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}