#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Moderation.DisconnectConnection;

/// <summary>
///     Represents a request to disconnect a connection.
/// </summary>
[Builder]
public readonly partial struct DisconnectConnectionRequest : IVonageRequest, IHasApplicationId, IHasSessionId,
    IHasConnectionId
{
    /// <inheritdoc />
    [Mandatory(0, nameof(VerifyApplicationId))]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(2, nameof(VerifyConnectionId))]
    public string ConnectionId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1, nameof(VerifySessionId))]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Delete, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}";

    internal static Result<DisconnectConnectionRequest> VerifyApplicationId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    internal static Result<DisconnectConnectionRequest> VerifyConnectionId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(request.ConnectionId));

    internal static Result<DisconnectConnectionRequest> VerifySessionId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}