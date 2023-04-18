using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Moderation.DisconnectConnection;

/// <summary>
///     Represents a request to disconnect a connection.
/// </summary>
public readonly struct DisconnectConnectionRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    private DisconnectConnectionRequest(Guid applicationId, string sessionId, string connectionId)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.ConnectionId = connectionId;
    }

    /// <inheritdoc />
    public Guid ApplicationId { get; }

    /// <summary>
    ///     The specific publisher connection Id.
    /// </summary>
    public string ConnectionId { get; }

    /// <inheritdoc />
    public string SessionId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Delete, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}";

    /// <summary>
    ///     Parses the input into a DisconnectConnectionRequest.
    /// </summary>
    /// <param name="applicationId">   The Vonage Application UUID.</param>
    /// <param name="sessionId">  The Video session Id.</param>
    /// <param name="connectionId"> The specific publisher connection Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DisconnectConnectionRequest>
        Parse(Guid applicationId, string sessionId, string connectionId) =>
        Result<DisconnectConnectionRequest>
            .FromSuccess(new DisconnectConnectionRequest(applicationId, sessionId, connectionId))
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(VerifyConnectionId)
            .Bind(BuilderExtensions.VerifySessionId);

    private static Result<DisconnectConnectionRequest> VerifyConnectionId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(ConnectionId));
}