using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.GetMembers;

internal class GetMembersRequestBuilder : IBuilderForConversationId, IBuilderForOptional
{
    private const int MaximumPageSize = 100;
    private const int MinimumPageSize = 1;
    private readonly Maybe<string> cursor;
    private string conversationId;
    private FetchOrder fetchOrder = FetchOrder.Ascending;
    private int pageSize = 10;
    
    internal GetMembersRequestBuilder(Maybe<string> cursor) => this.cursor = cursor;
    
    /// <inheritdoc />
    public IBuilderForOptional WithConversationId(string value) =>
        new GetMembersRequestBuilder(this.cursor)
        {
            pageSize = this.pageSize,
            fetchOrder = this.fetchOrder,
            conversationId = value,
        };
    
    /// <inheritdoc />
    public Result<GetMembersRequest> Create() => Result<GetMembersRequest>.FromSuccess(new GetMembersRequest
        {
            PageSize = this.pageSize,
            Cursor = this.cursor,
            ConversationId = this.conversationId,
            Order = this.fetchOrder,
        })
        .Map(InputEvaluation<GetMembersRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyConversationId, VerifyMinimumPageSize, VerifyMaximumPageSize));
    
    /// <inheritdoc />
    public IBuilderForOptional WithOrder(FetchOrder value) =>
        new GetMembersRequestBuilder(this.cursor)
        {
            pageSize = this.pageSize,
            fetchOrder = value,
            conversationId = this.conversationId,
        };
    
    /// <inheritdoc />
    public IBuilderForOptional WithPageSize(int value) =>
        new GetMembersRequestBuilder(this.cursor)
        {
            pageSize = value,
            fetchOrder = this.fetchOrder,
            conversationId = this.conversationId,
        };
    
    private static Result<GetMembersRequest> VerifyConversationId(GetMembersRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(request.ConversationId));
    
    private static Result<GetMembersRequest> VerifyMaximumPageSize(GetMembersRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.PageSize, MaximumPageSize, nameof(request.PageSize));
    
    private static Result<GetMembersRequest> VerifyMinimumPageSize(GetMembersRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.PageSize, MinimumPageSize, nameof(request.PageSize));
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetMembersRequest>
{
    /// <summary>
    ///     Sets the order on the builder.
    /// </summary>
    /// <param name="value">The order.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOrder(FetchOrder value);
    
    /// <summary>
    ///     Sets the page size on the builder.
    /// </summary>
    /// <param name="value">The page size.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPageSize(int value);
}

/// <summary>
///     Represents a builder for ConversationId.
/// </summary>
public interface IBuilderForConversationId
{
    /// <summary>
    ///     Sets the ConversationId on the builder.
    /// </summary>
    /// <param name="value">The conversation Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithConversationId(string value);
}