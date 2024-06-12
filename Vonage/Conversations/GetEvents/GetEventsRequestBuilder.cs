using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.GetEvents;

internal struct GetEventsRequestBuilder : IBuilderForConversationId, IBuilderForOptional
{
    private const int MaximumPageSize = 100;
    private const int MinimumPageSize = 1;
    private string conversationId;
    private FetchOrder fetchOrder = FetchOrder.Ascending;
    private int pageSize = 10;
    private Maybe<string> endId = Maybe<string>.None;
    private Maybe<string> startId = Maybe<string>.None;
    private Maybe<string> eventType = Maybe<string>.None;
    private bool excludeDeleted = false;
    private readonly Maybe<string> cursor;

    internal GetEventsRequestBuilder(Maybe<string> cursor) => this.cursor = cursor;

    public Result<GetEventsRequest> Create() => Result<GetEventsRequest>.FromSuccess(new GetEventsRequest
        {
            ConversationId = this.conversationId,
            ExcludeDeletedEvents = this.excludeDeleted,
            Cursor = this.cursor,
            Order = this.fetchOrder,
            PageSize = this.pageSize,
            EndId = this.endId,
            EventType = this.eventType,
            StartId = this.startId,
        })
        .Map(InputEvaluation<GetEventsRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyConversationId, VerifyMinimumPageSize, VerifyMaximumPageSize));

    public IBuilderForOptional WithEndId(string value) => this with {endId = value};
    public IBuilderForOptional WithOrder(FetchOrder value) => this with {fetchOrder = value};
    public IBuilderForOptional WithPageSize(int value) => this with {pageSize = value};
    public IBuilderForOptional WithStartId(string value) => this with {startId = value};
    public IBuilderForOptional WithEventType(string value) => this with {eventType = value};
    public IBuilderForOptional ExcludeDeletedEvents() => this with {excludeDeleted = true};
    public IBuilderForOptional WithConversationId(string value) => this with {conversationId = value};

    private static Result<GetEventsRequest> VerifyConversationId(GetEventsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(request.ConversationId));

    private static Result<GetEventsRequest> VerifyMaximumPageSize(GetEventsRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.PageSize, MaximumPageSize, nameof(request.PageSize));

    private static Result<GetEventsRequest> VerifyMinimumPageSize(GetEventsRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.PageSize, MinimumPageSize, nameof(request.PageSize));
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

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetEventsRequest>
{
    /// <summary>
    ///     Sets the end id on the builder.
    /// </summary>
    /// <param name="value">The end id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithEndId(string value);

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
    ///     Sets the start id on the builder.
    /// </summary>
    /// <param name="value">The start id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithStartId(string value);

    /// <summary>
    ///     Sets the event type on the builder.
    /// </summary>
    /// <param name="value">The event type.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithEventType(string value);

    /// <summary>
    ///     Sets builder to exclude deleted events.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional ExcludeDeletedEvents();
}