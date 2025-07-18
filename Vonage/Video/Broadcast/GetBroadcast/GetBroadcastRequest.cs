﻿#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Broadcast.GetBroadcast;

/// <summary>
///     Represents a request to retrieve a broadcast.
/// </summary>
[Builder]
public readonly partial struct GetBroadcastRequest : IVonageRequest, IHasApplicationId, IHasBroadcastId
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
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}";

    [ValidationRule]
    internal static Result<GetBroadcastRequest> VerifyApplicationId(GetBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<GetBroadcastRequest> VerifyBroadcastId(GetBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));
}