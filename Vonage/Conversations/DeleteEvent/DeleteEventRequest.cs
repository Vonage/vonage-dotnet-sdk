using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.DeleteEvent;

/// <inheritdoc />
public readonly struct DeleteEventRequest : IVonageRequest
{
    /// <summary>
    ///     The conversation Id.
    /// </summary>
    public string ConversationId { get; private init; }
    
    /// <summary>
    ///     The event Id.
    /// </summary>
    public string EventId { get; private init; }
    
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, this.GetEndpointPath())
        .Build();
    
    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/conversations/{this.ConversationId}/events/{this.EventId}";
    
    /// <summary>
    ///     Parses the input into a DeleteEventRequest.
    /// </summary>
    /// <param name="conversationId">The conversation Id.</param>
    /// <param name="eventId">The event Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DeleteEventRequest> Parse(string conversationId, string eventId) =>
        Result<DeleteEventRequest>
            .FromSuccess(new DeleteEventRequest {ConversationId = conversationId, EventId = eventId})
            .Map(InputEvaluation<DeleteEventRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyConversationId, VerifyEventId));
    
    private static Result<DeleteEventRequest> VerifyConversationId(DeleteEventRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(ConversationId));
    
    private static Result<DeleteEventRequest> VerifyEventId(DeleteEventRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.EventId, nameof(EventId));
}