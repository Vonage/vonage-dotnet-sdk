using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Signaling.Common;

namespace Vonage.Server.Video.Signaling.SendSignal;

/// <summary>
///     Represents a request to send a signal to specific participant.
/// </summary>
public readonly struct SendSignalRequest : IVonageRequest
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
    ///     The specific publisher connection Id.
    /// </summary>
    public string ConnectionId { get; }

    /// <summary>
    ///     The signal content.
    /// </summary>
    public SignalContent Content { get; }

    /// <summary>
    ///     The Video session Id.
    /// </summary>
    public string SessionId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}/signal";

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

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this.Content),
            Encoding.UTF8,
            "application/json");

    private static Result<SendSignalRequest> VerifyApplicationId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<SendSignalRequest> VerifyConnectionId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(ConnectionId));

    private static Result<SendSignalRequest> VerifyContentData(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));

    private static Result<SendSignalRequest> VerifyContentType(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));

    private static Result<SendSignalRequest> VerifySessionId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));
}