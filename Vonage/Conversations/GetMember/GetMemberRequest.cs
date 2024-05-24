using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.GetMember;

/// <inheritdoc />
public readonly struct GetMemberRequest : IVonageRequest
{
    /// <summary>
    ///     The conversation Id.
    /// </summary>
    public string ConversationId { get; private init; }
    
    /// <summary>
    ///     The member Id.
    /// </summary>
    public string MemberId { get; private init; }
    
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();
    
    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/conversations/{this.ConversationId}/members/{this.MemberId}";
    
    /// <summary>
    ///     Parses the input into a GetMemberRequest.
    /// </summary>
    /// <param name="conversationId">The conversation Id.</param>
    /// <param name="memberId">The member Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetMemberRequest> Parse(string conversationId, string memberId) =>
        Result<GetMemberRequest>
            .FromSuccess(new GetMemberRequest {ConversationId = conversationId, MemberId = memberId})
            .Map(InputEvaluation<GetMemberRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyConversationId, VerifyMemberId));
    
    private static Result<GetMemberRequest> VerifyConversationId(GetMemberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(ConversationId));
    
    private static Result<GetMemberRequest> VerifyMemberId(GetMemberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.MemberId, nameof(MemberId));
}