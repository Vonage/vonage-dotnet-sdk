using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Items.GetItem;

internal class DeleteItemRequestBuilder : IBuilderForListId, IBuilderForItemId, IVonageRequestBuilder<GetItemRequest>
{
    private Guid listId;
    private Guid itemId;

    /// <inheritdoc />
    public Result<GetItemRequest> Create() =>
        Result<GetItemRequest>
            .FromSuccess(new GetItemRequest
            {
                ListId = this.listId,
                ItemId = this.itemId,
            })
            .Bind(VerifyListId)
            .Bind(VerifyItemId);

    public IVonageRequestBuilder<GetItemRequest> WithItemId(Guid value)
    {
        this.itemId = value;
        return this;
    }

    public IBuilderForItemId WithListId(Guid value)
    {
        this.listId = value;
        return this;
    }

    private static Result<GetItemRequest> VerifyItemId(GetItemRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ItemId, nameof(request.ItemId));

    private static Result<GetItemRequest> VerifyListId(GetItemRequest request) =>
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
    IVonageRequestBuilder<GetItemRequest> WithItemId(Guid value);
}