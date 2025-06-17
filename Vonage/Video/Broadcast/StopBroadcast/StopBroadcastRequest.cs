#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Broadcast.StopBroadcast;

/// <summary>
///     Represents a request to stop a broadcast.
/// </summary>
[Builder]
public readonly partial struct StopBroadcastRequest : IVonageRequest, IHasApplicationId, IHasBroadcastId
{
    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1)]
    public Guid BroadcastId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}/stop";

    [ValidationRule]
    internal static Result<StopBroadcastRequest> VerifyApplicationId(StopBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<StopBroadcastRequest> VerifyBroadcastId(StopBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));
}