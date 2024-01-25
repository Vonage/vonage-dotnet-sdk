using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.GetUserConversations;

internal class GetUserConversationsRequestBuilder : IBuilderForUserId, IBuilderForOptional
{
    private const string DefaultOrderBy = "created";
    private const int MaximumPageSize = 100;
    private const int MinimumPageSize = 1;
    private readonly Maybe<string> cursor;
    private bool includeCustomData;
    private FetchOrder order = FetchOrder.Ascending;
    private string orderBy = DefaultOrderBy;
    private int pageSize = 10;
    private Maybe<DateTimeOffset> startDate;
    private Maybe<State> state;
    private string userId;

    internal GetUserConversationsRequestBuilder(Maybe<string> cursor) => this.cursor = cursor;

    public Result<GetUserConversationsRequest> Create() => Result<GetUserConversationsRequest>.FromSuccess(
            new GetUserConversationsRequest
            {
                UserId = this.userId,
                PageSize = this.pageSize,
                IncludeCustomData = this.includeCustomData,
                StartDate = this.startDate,
                Order = this.order,
                OrderBy = this.orderBy,
                State = this.state,
                Cursor = this.cursor,
            })
        .Map(InputEvaluation<GetUserConversationsRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyUserId,
            VerifyMinimumPageSize,
            VerifyMaximumPageSize));

    /// <inheritdoc />
    public IBuilderForOptional WithOrder(FetchOrder value)
    {
        this.order = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithOrderBy(Maybe<string> value)
    {
        this.orderBy = value.IfNone(DefaultOrderBy);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithState(State value)
    {
        this.state = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional IncludeCustomData()
    {
        this.includeCustomData = true;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithPageSize(int value)
    {
        this.pageSize = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithStartDate(DateTimeOffset value)
    {
        this.startDate = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithUserId(string value)
    {
        this.userId = value;
        return this;
    }

    private static Result<GetUserConversationsRequest> VerifyMaximumPageSize(GetUserConversationsRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.PageSize, MaximumPageSize, nameof(request.PageSize));

    private static Result<GetUserConversationsRequest> VerifyUserId(GetUserConversationsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.UserId, nameof(request.UserId));

    private static Result<GetUserConversationsRequest> VerifyMinimumPageSize(GetUserConversationsRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.PageSize, MinimumPageSize, nameof(request.PageSize));
}

/// <summary>
///     Represents a builder for User Id.
/// </summary>
public interface IBuilderForUserId
{
    /// <summary>
    ///     Sets the User Id on the builder.
    /// </summary>
    /// <param name="value">The user id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithUserId(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetUserConversationsRequest>
{
    /// <summary>
    ///     Sets the order on the builder.
    /// </summary>
    /// <param name="value">The order.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOrder(FetchOrder value);

    /// <summary>
    ///     Sets the OrderBy on the builder.
    /// </summary>
    /// <param name="value">The order by.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOrderBy(Maybe<string> value);

    /// <summary>
    ///     Sets the state on the builder.
    /// </summary>
    /// <param name="value">The state.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithState(State value);

    /// <summary>
    ///     Sets the request to include custom data.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional IncludeCustomData();

    /// <summary>
    ///     Sets the page size on the builder.
    /// </summary>
    /// <param name="value">The page size.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPageSize(int value);

    /// <summary>
    ///     Sets the start date on the builder.
    /// </summary>
    /// <param name="value">The start date.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithStartDate(DateTimeOffset value);
}