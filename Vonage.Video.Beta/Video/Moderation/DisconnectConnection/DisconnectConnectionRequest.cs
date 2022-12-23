using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Moderation.DisconnectConnection;

/// <summary>
///     Represents a request to disconnect a connection.
/// </summary>
public readonly struct DisconnectConnectionRequest : IVideoRequest
{
    private DisconnectConnectionRequest(string applicationId, string sessionId, string connectionId)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.ConnectionId = connectionId;
    }

    /// <summary>
    ///     The Vonage Application UUID.
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
    ///     Parses the input into a DisconnectConnectionRequest.
    /// </summary>
    /// <param name="applicationId">   The Vonage Application UUID.</param>
    /// <param name="sessionId">  The Video session Id.</param>
    /// <param name="connectionId"> The specific publisher connection Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DisconnectConnectionRequest>
        Parse(string applicationId, string sessionId, string connectionId) =>
        Result<DisconnectConnectionRequest>
            .FromSuccess(new DisconnectConnectionRequest(applicationId, sessionId, connectionId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyConnectionId)
            .Bind(VerifySessionId);

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, this.GetEndpointPath());
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    private static Result<DisconnectConnectionRequest> VerifyApplicationId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<DisconnectConnectionRequest> VerifySessionId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    private static Result<DisconnectConnectionRequest> VerifyConnectionId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(ConnectionId));
}