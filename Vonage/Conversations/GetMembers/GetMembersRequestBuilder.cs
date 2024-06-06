using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.GetMembers;

internal struct GetMembersRequestBuilder : IBuilderForConversationId, IBuilderForOptional
{
    private const int MaximumPageSize = 100;
    private const int MinimumPageSize = 1;
    private FetchOrder fetchOrder = FetchOrder.Ascending;
    private int pageSize = 10;
    private readonly Maybe<string> cursor;
    private string conversationId;

    internal GetMembersRequestBuilder(Maybe<string> cursor) => this.cursor = cursor;

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
    public IBuilderForOptional WithConversationId(string value) => this with {conversationId = value};

    /// <inheritdoc />
    public IBuilderForOptional WithOrder(FetchOrder value) => this with {fetchOrder = value};

    /// <inheritdoc />
    public IBuilderForOptional WithPageSize(int value) => this with {pageSize = value};

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