using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Items.UpdateItem;

internal class UpdateItemRequestBuilder : IBuilderForListId, IBuilderForItemId, IBuilderForCustomData
{
    private readonly Dictionary<string, object> data = new();
    private Guid listId;
    private Guid itemId;

    /// <inheritdoc />
    public Result<UpdateItemRequest> Create() =>
        Result<UpdateItemRequest>
            .FromSuccess(new UpdateItemRequest
            {
                ListId = this.listId,
                ItemId = this.itemId,
                Data = new ReadOnlyDictionary<string, object>(this.data),
            })
            .Map(InputEvaluation<UpdateItemRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyListId, VerifyItemId, VerifyData));

    /// <inheritdoc />
    public IBuilderForCustomData WithCustomData(KeyValuePair<string, object> value)
    {
        this.data.Add(value.Key, value.Value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForCustomData WithItemId(Guid value)
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

    private static Result<UpdateItemRequest> VerifyData(UpdateItemRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Data, nameof(request.Data));

    private static Result<UpdateItemRequest> VerifyItemId(UpdateItemRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ItemId, nameof(request.ItemId));

    private static Result<UpdateItemRequest> VerifyListId(UpdateItemRequest request) =>
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
    IBuilderForCustomData WithItemId(Guid value);
}

/// <summary>
///     Represents a builder for custom Data.
/// </summary>
public interface IBuilderForCustomData : IVonageRequestBuilder<UpdateItemRequest>
{
    /// <summary>
    ///     Sets the custom Data on the builder.
    /// </summary>
    /// <param name="value">The custom Data.</param>
    /// <returns>The builder.</returns>
    IBuilderForCustomData WithCustomData(KeyValuePair<string, object> value);
}