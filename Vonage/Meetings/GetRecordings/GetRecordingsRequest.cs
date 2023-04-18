using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRecordings;

/// <summary>
///     Represents a request to retrieve recordings from a session.
/// </summary>
public readonly struct GetRecordingsRequest : IVonageRequest, IHasSessionId
{
    private GetRecordingsRequest(string sessionId) => this.SessionId = sessionId;

    /// <inheritdoc />
    public string SessionId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/beta/meetings/sessions/{this.SessionId}/recordings";

    /// <summary>
    ///     Parses the input into a GetRecordingsRequest.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetRecordingsRequest> Parse(string sessionId) =>
        Result<GetRecordingsRequest>
            .FromSuccess(new GetRecordingsRequest(sessionId))
            .Bind(BuilderExtensions.VerifySessionId);
}