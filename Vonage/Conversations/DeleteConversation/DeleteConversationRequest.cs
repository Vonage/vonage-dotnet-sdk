#region
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Conversations.DeleteConversation;

/// <inheritdoc />
public readonly struct DeleteConversationRequest : IVonageRequest
{
    /// <summary>
    ///     The conversation Id.
    /// </summary>
    public string ConversationId { get; private init; }

    /// <summary>
    ///     Parses the input into a DeleteConversationRequest.
    /// </summary>
    /// <param name="conversationId">The conversation Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DeleteConversationRequest> Parse(string conversationId) =>
        Result<DeleteConversationRequest>
            .FromSuccess(new DeleteConversationRequest {ConversationId = conversationId})
            .Map(InputEvaluation<DeleteConversationRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyConversationId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, $"/v1/conversations/{this.ConversationId}")
        .Build();

    private static Result<DeleteConversationRequest> VerifyConversationId(DeleteConversationRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(ConversationId));
}