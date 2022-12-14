using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

/// <summary>
///     Represents a request to retrieve a stream.
/// </summary>
public readonly struct GetStreamRequest
{
    private GetStreamRequest(string applicationId, string sessionId, string streamId)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.StreamId = streamId;
    }

    private const string CannotBeNullOrWhitespace = "cannot be null or whitespace.";

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
        VerifyNotEmptyValue(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<GetStreamRequest> VerifyNotEmptyValue(GetStreamRequest request, string value, string name) =>
        string.IsNullOrWhiteSpace(value)
            ? Result<GetStreamRequest>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CannotBeNullOrWhitespace}"))
            : request;

    private static Result<GetStreamRequest> VerifySessionId(GetStreamRequest request) =>
        VerifyNotEmptyValue(request, request.SessionId, nameof(SessionId));

    private static Result<GetStreamRequest> VerifyStreamId(GetStreamRequest request) =>
        VerifyNotEmptyValue(request, request.StreamId, nameof(StreamId));

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
}