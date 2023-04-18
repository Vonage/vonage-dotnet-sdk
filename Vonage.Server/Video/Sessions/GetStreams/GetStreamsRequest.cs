using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Sessions.GetStreams;

/// <summary>
///     Represents a request to retrieve streams.
/// </summary>
public readonly struct GetStreamsRequest : IVonageRequest, IHasApplicationId
{
    private GetStreamsRequest(Guid applicationId, string sessionId)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
    }

    /// <inheritdoc />
    public Guid ApplicationId { get; }

    /// <summary>
    ///     The session Id.
    /// </summary>
    public string SessionId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream";

    /// <summary>
    ///     Parses the input into a GetStreamRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="sessionId">The session Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetStreamsRequest> Parse(Guid applicationId, string sessionId) =>
        Result<GetStreamsRequest>
            .FromSuccess(new GetStreamsRequest(applicationId, sessionId))
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(VerifySessionId);

    private static Result<GetStreamsRequest> VerifySessionId(GetStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));
}