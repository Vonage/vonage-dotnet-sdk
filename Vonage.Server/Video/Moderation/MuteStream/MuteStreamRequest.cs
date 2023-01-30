using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Moderation.MuteStream;

/// <summary>
///     Represents a request to mute a stream.
/// </summary>
public readonly struct MuteStreamRequest : IVonageRequest
{
    private MuteStreamRequest(string applicationId, string sessionId, string streamId)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.StreamId = streamId;
    }

    /// <summary>
    ///      The Vonage Application UUID.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///       The Video session Id.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     The stream Id.
    /// </summary>
    public string StreamId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream/{this.StreamId}/mute";

    /// <summary>
    ///     Parses the input into a MuteStreamRequest.
    /// </summary>
    /// <param name="applicationId"> The Vonage Application UUID.</param>
    /// <param name="sessionId">   The Video session Id.</param>
    /// <param name="streamId">The stream Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<MuteStreamRequest> Parse(string applicationId, string sessionId, string streamId) =>
        Result<MuteStreamRequest>
            .FromSuccess(new MuteStreamRequest(applicationId, sessionId, streamId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyStreamId)
            .Bind(VerifySessionId);

    private static Result<MuteStreamRequest> VerifyApplicationId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<MuteStreamRequest> VerifySessionId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    private static Result<MuteStreamRequest> VerifyStreamId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}