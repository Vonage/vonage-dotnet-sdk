#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Sessions.GetStream;

/// <summary>
///     Represents a request to retrieve a stream.
/// </summary>
[Builder]
public readonly partial struct GetStreamRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <summary>
    ///     The stream Id.
    /// </summary>
    [Mandatory(2, nameof(VerifyStreamId))]
    public string StreamId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0, nameof(VerifyApplicationId))]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1, nameof(VerifySessionId))]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream/{this.StreamId}";

    internal static Result<GetStreamRequest> VerifyApplicationId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    internal static Result<GetStreamRequest> VerifySessionId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    internal static Result<GetStreamRequest> VerifyStreamId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}