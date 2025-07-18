﻿#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Moderation.MuteStream;

/// <summary>
///     Represents a request to mute a stream.
/// </summary>
[Builder]
public readonly partial struct MuteStreamRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <summary>
    ///     The stream Id.
    /// </summary>
    [Mandatory(2)]
    public string StreamId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream/{this.StreamId}/mute";

    [ValidationRule]
    internal static Result<MuteStreamRequest> VerifyApplicationId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<MuteStreamRequest> VerifySessionId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    [ValidationRule]
    internal static Result<MuteStreamRequest> VerifyStreamId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}