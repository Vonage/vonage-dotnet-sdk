#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Sessions.GetStreams;

/// <summary>
///     Represents a request to retrieve streams.
/// </summary>
[Builder]
public readonly partial struct GetStreamsRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
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
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream";

    [ValidationRule]
    internal static Result<GetStreamsRequest> VerifyApplicationId(GetStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<GetStreamsRequest> VerifySessionId(GetStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}