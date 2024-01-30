﻿using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.GetConversation;

/// <inheritdoc />
public readonly struct GetConversationRequest : IVonageRequest
{
    /// <summary>
    ///     The conversation Id.
    /// </summary>
    public string ConversationId { get; private init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/conversations/{this.ConversationId}";

    /// <summary>
    ///     Parses the input into a GetConversationRequest.
    /// </summary>
    /// <param name="conversationId">The conversation Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetConversationRequest> Parse(string conversationId) =>
        Result<GetConversationRequest>
            .FromSuccess(new GetConversationRequest {ConversationId = conversationId})
            .Map(InputEvaluation<GetConversationRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyConversationId));

    private static Result<GetConversationRequest> VerifyConversationId(GetConversationRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(ConversationId));
}