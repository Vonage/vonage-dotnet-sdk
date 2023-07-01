using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Items.CreateItem;

internal class CreateItemRequestBuilder : IBuilderForListId, IBuilderForCustomData
{
    private readonly Dictionary<string, object> data = new();
    private Guid listId;

    /// <inheritdoc />
    public Result<CreateItemRequest> Create() =>
        Result<CreateItemRequest>
            .FromSuccess(new CreateItemRequest
            {
                ListId = this.listId,
                Data = new ReadOnlyDictionary<string, object>(this.data),
            })
            .Map(InputEvaluation<CreateItemRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyListId, VerifyData));

    /// <inheritdoc />
    public IBuilderForCustomData WithCustomData(KeyValuePair<string, object> value)
    {
        this.data.Add(value.Key, value.Value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForCustomData WithListId(Guid value)
    {
        this.listId = value;
        return this;
    }

    private static Result<CreateItemRequest> VerifyData(CreateItemRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Data, nameof(request.Data));

    private static Result<CreateItemRequest> VerifyListId(CreateItemRequest request) =>
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
    IBuilderForCustomData WithListId(Guid value);
}

/// <summary>
///     Represents a builder for custom Data.
/// </summary>
public interface IBuilderForCustomData : IVonageRequestBuilder<CreateItemRequest>
{
    /// <summary>
    ///     Sets the custom Data on the builder.
    /// </summary>
    /// <param name="value">The custom Data.</param>
    /// <returns>The builder.</returns>
    IBuilderForCustomData WithCustomData(KeyValuePair<string, object> value);
}