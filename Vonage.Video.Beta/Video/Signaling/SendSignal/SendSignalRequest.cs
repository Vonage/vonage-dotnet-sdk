using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Signaling.SendSignal;

/// <summary>
///     Represents a request to send a signal to specific participant.
/// </summary>
public readonly struct SendSignalRequest : IVideoRequest
{
    private SendSignalRequest(string applicationId, string sessionId, string connectionId, SignalContent content)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.ConnectionId = connectionId;
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
    ///     The specific publisher connection Id.
    /// </summary>
    public string ConnectionId { get; }

    /// <summary>
    ///     The signal content.
    /// </summary>
    public SignalContent Content { get; }

    /// <summary>
    ///     Parses the input into a SendSignalRequest.
    /// </summary>
    /// <param name="applicationId">The Vonage application UUID.</param>
    /// <param name="sessionId">The Video session Id.</param>
    /// <param name="connectionId">The specific publisher connection Id.</param>
    /// <param name="content"> The signal content.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<SendSignalRequest> Parse(string applicationId, string sessionId, string connectionId,
        SignalContent content) =>
        Result<SendSignalRequest>
            .FromSuccess(new SendSignalRequest(applicationId, sessionId, connectionId, content))
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyConnectionId)
            .Bind(VerifyContentType)
            .Bind(VerifyContentData);

    /// <summary>
    ///     Retrieves the endpoint's path.
    /// </summary>
    /// <returns>The endpoint's path.</returns>
    public string GetEndpointPath() =>
        $"/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}/signal";

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

    private static Result<SendSignalRequest> VerifyApplicationId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<SendSignalRequest> VerifySessionId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    private static Result<SendSignalRequest> VerifyContentType(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));

    private static Result<SendSignalRequest> VerifyContentData(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));

    private static Result<SendSignalRequest> VerifyConnectionId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(ConnectionId));
}