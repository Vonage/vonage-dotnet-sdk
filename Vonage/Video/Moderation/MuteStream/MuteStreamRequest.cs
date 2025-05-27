#region
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
            .Initialize(HttpMethod.Post,
                $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream/{this.StreamId}/mute")
            .Build();

    internal static Result<MuteStreamRequest> VerifyApplicationId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    internal static Result<MuteStreamRequest> VerifySessionId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    internal static Result<MuteStreamRequest> VerifyStreamId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}