using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.DeleteConversation
{
    public readonly struct DeleteConversationRequest : IVonageRequest
    {
        public string ConversationId { get; private init; }
        public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

        public string GetEndpointPath() => throw new NotImplementedException();

        public static Result<DeleteConversationRequest> Parse(string conversationId) =>
            Result<DeleteConversationRequest>
                .FromSuccess(new DeleteConversationRequest {ConversationId = conversationId})
                .Map(InputEvaluation<DeleteConversationRequest>.Evaluate)
                .Bind(evaluation => evaluation.WithRules(VerifyConversationId));

        private static Result<DeleteConversationRequest> VerifyConversationId(DeleteConversationRequest request) =>
            InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(ConversationId));
    }
}