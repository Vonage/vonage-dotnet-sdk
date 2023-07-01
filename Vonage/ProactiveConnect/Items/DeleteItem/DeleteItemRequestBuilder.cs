using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Items.DeleteItem;

internal class DeleteItemRequestBuilder : IBuilderForListId, IBuilderForItemId, IVonageRequestBuilder<DeleteItemRequest>
{
    private Guid listId;
    private Guid itemId;

    /// <inheritdoc />
    public Result<DeleteItemRequest> Create() =>
        Result<DeleteItemRequest>
            .FromSuccess(new DeleteItemRequest
            {
                ListId = this.listId,
                ItemId = this.itemId,
            })
            .Map(InputEvaluation<DeleteItemRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyListId, VerifyItemId));

    /// <inheritdoc />
    public IVonageRequestBuilder<DeleteItemRequest> WithItemId(Guid value)
    {
        this.itemId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForItemId WithListId(Guid value)
    {
        this.listId = value;
        return this;
    }

    private static Result<DeleteItemRequest> VerifyItemId(DeleteItemRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ItemId, nameof(request.ItemId));

    private static Result<DeleteItemRequest> VerifyListId(DeleteItemRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ListId, nameof(request.ListId));
}

/// <summary>
///     Represents a builder for ListId.
/// </summary>
public interface IBuilderForListId
{
    /// <summary>
    ///     Sets the ListId.
    /// </summary>
    /// <param name="value">The list Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForItemId WithListId(Guid value);
}

/// <summary>
///     Represents a builder for ItemId.
/// </summary>
public interface IBuilderForItemId
{
    /// <summary>
    ///     Sets the ItemId on the builder.
    /// </summary>
    /// <param name="value">The ItemId.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<DeleteItemRequest> WithItemId(Guid value);
}