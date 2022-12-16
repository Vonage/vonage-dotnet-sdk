using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.GetStreams;

public readonly struct GetStreamsRequest
{
    private GetStreamsRequest(string applicationId, string sessionId)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
    }

    private const string CannotBeNullOrWhitespace = "cannot be null or whitespace.";

    /// <summary>
    ///     Parses the input into a GetStreamRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="sessionId">The session Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetStreamsRequest> Parse(string applicationId, string sessionId) =>
        Result<GetStreamsRequest>
            .FromSuccess(new GetStreamsRequest(applicationId, sessionId))
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId);

    private static Result<GetStreamsRequest> VerifyApplicationId(GetStreamsRequest request) =>
        VerifyNotEmptyValue(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<GetStreamsRequest>
        VerifyNotEmptyValue(GetStreamsRequest request, string value, string name) =>
        string.IsNullOrWhiteSpace(value)
            ? Result<GetStreamsRequest>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CannotBeNullOrWhitespace}"))
            : request;

    private static Result<GetStreamsRequest> VerifySessionId(GetStreamsRequest request) =>
        VerifyNotEmptyValue(request, request.SessionId, nameof(SessionId));

    /// <summary>
    ///     The application Id.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The session Id.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     Retrieves the endpoint's path.
    /// </summary>
    /// <returns>The endpoint's path.</returns>
    public string GetEndpointPath() => $"/project/{this.ApplicationId}/session/{this.SessionId}/stream";
}