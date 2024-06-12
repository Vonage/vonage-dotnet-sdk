using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.GetConversations;

internal struct GetConversationsRequestBuilder : IBuilderForOptional
{
    private const int MaximumPageSize = 100;
    private const int MinimumPageSize = 1;
    private FetchOrder fetchOrder = FetchOrder.Ascending;
    private int pageSize = 10;
    private Maybe<DateTimeOffset> endDate;
    private Maybe<DateTimeOffset> startDate;
    private readonly Maybe<string> cursor;

    internal GetConversationsRequestBuilder(Maybe<string> cursor) => this.cursor = cursor;

    public Result<GetConversationsRequest> Create() => Result<GetConversationsRequest>.FromSuccess(
            new GetConversationsRequest
            {
                StartDate = this.startDate,
                Order = this.fetchOrder,
                PageSize = this.pageSize,
                Cursor = this.cursor,
                EndDate = this.endDate,
            })
        .Map(InputEvaluation<GetConversationsRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyMinimumPageSize, VerifyMaximumPageSize));

    public IBuilderForOptional WithEndDate(DateTimeOffset value) => this with {endDate = value};

    public IBuilderForOptional WithOrder(FetchOrder value) => this with {fetchOrder = value};

    public IBuilderForOptional WithPageSize(int value) => this with {pageSize = value};

    public IBuilderForOptional WithStartDate(DateTimeOffset value) => this with {startDate = value};

    private static Result<GetConversationsRequest> VerifyMaximumPageSize(GetConversationsRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.PageSize, MaximumPageSize, nameof(request.PageSize));

    private static Result<GetConversationsRequest> VerifyMinimumPageSize(GetConversationsRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.PageSize, MinimumPageSize, nameof(request.PageSize));
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetConversationsRequest>
{
    /// <summary>
    ///     Sets the end date on the builder.
    /// </summary>
    /// <param name="value">The end date.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithEndDate(DateTimeOffset value);

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

    /// <summary>
    ///     Sets the start date on the builder.
    /// </summary>
    /// <param name="value">The start date.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithStartDate(DateTimeOffset value);
}