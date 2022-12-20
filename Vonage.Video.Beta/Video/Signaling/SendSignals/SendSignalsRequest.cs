using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

public readonly struct SendSignalsRequest
{
    private SendSignalsRequest(string applicationId, string sessionId, SignalContent content)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.Content = content;
    }

    private const string CannotBeNullOrWhitespace = "cannot be null or whitespace.";

    public static Result<SendSignalsRequest> Parse(string applicationId, string sessionId, SignalContent content) =>
        Result<SendSignalsRequest>
            .FromSuccess(new SendSignalsRequest(applicationId, sessionId, content))
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyContentType)
            .Bind(VerifyContentData);

    /// <summary>
    ///     Retrieves the endpoint's path.
    /// </summary>
    /// <returns>The endpoint's path.</returns>
    public string GetEndpointPath() => $"/project/{this.ApplicationId}/session/{this.SessionId}/signal";

    private static Result<SendSignalsRequest> VerifyApplicationId(SendSignalsRequest request) =>
        VerifyNotEmptyValue(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<SendSignalsRequest>
        VerifyNotEmptyValue(SendSignalsRequest request, string value, string name) =>
        string.IsNullOrWhiteSpace(value)
            ? Result<SendSignalsRequest>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CannotBeNullOrWhitespace}"))
            : request;

    private static Result<SendSignalsRequest> VerifySessionId(SendSignalsRequest request) =>
        VerifyNotEmptyValue(request, request.SessionId, nameof(SessionId));

    private static Result<SendSignalsRequest> VerifyContentType(SendSignalsRequest request) =>
        VerifyNotEmptyValue(request, request.Content.Type, nameof(SignalContent.Type));

    private static Result<SendSignalsRequest> VerifyContentData(SendSignalsRequest request) =>
        VerifyNotEmptyValue(request, request.Content.Data, nameof(SignalContent.Data));

    public string ApplicationId { get; }
    public string SessionId { get; }
    public SignalContent Content { get; }

    public readonly struct SignalContent
    {
        public SignalContent(string type, string data)
        {
            this.Type = type;
            this.Data = data;
        }

        public string Type { get; }
        public string Data { get; }
    }
}