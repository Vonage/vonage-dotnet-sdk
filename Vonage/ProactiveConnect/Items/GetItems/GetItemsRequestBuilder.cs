using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Items.GetItems;

internal class GetItemsRequestBuilder : IBuilderForListId, IBuilderForPage, IBuilderForPageSize,
    IVonageRequestBuilder<GetItemsRequest>
{
    private Guid listId;
    private int pageSize;
    private int page;

    /// <inheritdoc />
    public Result<GetItemsRequest> Create() =>
        Result<GetItemsRequest>
            .FromSuccess(new GetItemsRequest
            {
                ListId = this.listId,
                Page = this.page,
                PageSize = this.pageSize,
            })
            .Bind(VerifyListId);

    public IBuilderForPage WithListId(Guid value)
    {
        this.listId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForPageSize WithPage(int value)
    {
        this.page = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<GetItemsRequest> WithPageSize(int value)
    {
        this.pageSize = value;
        return this;
    }

    private static Result<GetItemsRequest> VerifyListId(GetItemsRequest request) =>
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
    IBuilderForPage WithListId(Guid value);
}

/// <summary>
///     Represents a builder for Page.
/// </summary>
public interface IBuilderForPage
{
    /// <summary>
    ///     Sets the Page.
    /// </summary>
    /// <param name="value">The page.</param>
    /// <returns>The builder.</returns>
    IBuilderForPageSize WithPage(int value);
}

/// <summary>
///     Represents a builder for PageSize.
/// </summary>
public interface IBuilderForPageSize
{
    /// <summary>
    ///     Sets the PageSize on the builder.
    /// </summary>
    /// <param name="value">The page size.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<GetItemsRequest> WithPageSize(int value);
}