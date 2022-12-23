using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

/// <summary>
///     Represents a request to send a signal to all participants.
/// </summary>
public readonly struct SendSignalsRequest : IVideoRequest
{
    private SendSignalsRequest(string applicationId, string sessionId, SignalContent content)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.Content = content;
    }

    /// <summary>
    ///     The Vonage application UUID.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The Video session Id.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     The signal content.
    /// </summary>
    public SignalContent Content { get; }

    /// <summary>
    ///     Parses the input into a SendSignalsRequest.
    /// </summary>
    /// <param name="applicationId">The Vonage application UUID.</param>
    /// <param name="sessionId">The Video session Id.</param>
    /// <param name="content"> The signal content.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
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

    /// <summary>
    ///     Creates a Http request for retrieving a stream.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The Http request.</returns>
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Content = new StringContent(new JsonSerializer().SerializeObject(this.Content), Encoding.UTF8,
            "application/json");
        return httpRequest;
    }

    private static Result<SendSignalsRequest> VerifyApplicationId(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<SendSignalsRequest> VerifySessionId(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    private static Result<SendSignalsRequest> VerifyContentType(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));

    private static Result<SendSignalsRequest> VerifyContentData(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));
}