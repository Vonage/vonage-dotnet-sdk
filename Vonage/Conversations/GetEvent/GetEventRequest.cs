#region
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Conversations.GetEvent;

/// <inheritdoc />
public readonly struct GetEventRequest : IVonageRequest
{
    /// <summary>
    ///     The conversation Id.
    /// </summary>
    public string ConversationId { get; private init; }

    /// <summary>
    ///     The event Id.
    /// </summary>
    public string EventId { get; private init; }

    /// <summary>
    ///     Parses the input into a GetEventRequest.
    /// </summary>
    /// <param name="conversationId">The conversation Id.</param>
    /// <param name="eventId">The event Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetEventRequest> Parse(string conversationId, string eventId) =>
        Result<GetEventRequest>
            .FromSuccess(new GetEventRequest {ConversationId = conversationId, EventId = eventId})
            .Map(InputEvaluation<GetEventRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyConversationId, VerifyEventId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/v1/conversations/{this.ConversationId}/events/{this.EventId}")
        .Build();

    private static Result<GetEventRequest> VerifyConversationId(GetEventRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(ConversationId));

    private static Result<GetEventRequest> VerifyEventId(GetEventRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.EventId, nameof(EventId));
}