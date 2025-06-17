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
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream/{this.StreamId}";

    [ValidationRule]
    internal static Result<GetStreamRequest> VerifyApplicationId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<GetStreamRequest> VerifySessionId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    [ValidationRule]
    internal static Result<GetStreamRequest> VerifyStreamId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}