#region
using System;
using System.Text.Json.Serialization;
using System.Web;
using Vonage.Common;
using Vonage.Common.Monads;
#endregion

namespace Vonage.VerifyV2.GetTemplates;

/// <summary>
///     Represents a paginated response containing a list of custom verification templates.
/// </summary>
/// <param name="PageSize">The number of templates returned per page.</param>
/// <param name="Page">The current page number (1-based index).</param>
/// <param name="TotalPages">The total number of pages available.</param>
/// <param name="TotalItems">The total number of templates across all pages.</param>
/// <param name="Embedded">The embedded collection containing the template array.</param>
/// <param name="Links">HAL navigation links for pagination (self, next, prev, first, last).</param>
public record GetTemplatesResponse(
    [property: JsonPropertyName("page_size")]
    [property: JsonPropertyOrder(0)]
    int PageSize,
    [property: JsonPropertyName("page")]
    [property: JsonPropertyOrder(1)]
    int Page,
    [property: JsonPropertyName("total_pages")]
    [property: JsonPropertyOrder(2)]
    int TotalPages,
    [property: JsonPropertyName("total_items")]
    [property: JsonPropertyOrder(3)]
    int TotalItems,
    [property: JsonPropertyName("_embedded")]
    [property: JsonPropertyOrder(4)]
    GetTemplatesEmbedded Embedded,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(5)]
    HalLinks<GetTemplatesHalLink> Links);

/// <summary>
///     Represents the embedded container holding the templates array in a HAL response.
/// </summary>
/// <param name="Templates">The array of templates in the current page.</param>
public record GetTemplatesEmbedded(
    [property: JsonPropertyName("templates")]
    [property: JsonPropertyOrder(0)]
    Template[] Templates);

/// <summary>
///     Represents a HAL navigation link for template pagination.
/// </summary>
/// <param name="Href">The URL pointing to a page of templates.</param>
public record GetTemplatesHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms this navigation link into a <see cref="GetTemplatesRequest"/> for fetching the linked page.
    /// </summary>
    /// <returns>A <see cref="Result{T}"/> containing the request with pagination parameters extracted from the link URL.</returns>
    public Result<GetTemplatesRequest> BuildRequest()
    {
        var parameters = ExtractQueryParameters(this.Href);
        IBuilderForOptional builder = new GetTemplatesRequestBuilder();
        builder = parameters.ApplyPageSize(builder);
        builder = parameters.ApplyPage(builder);
        return builder.Create();
    }

    private static QueryParameters ExtractQueryParameters(Uri uri)
    {
        var queryParameters = HttpUtility.ParseQueryString(uri.Query);
        var pageSize = queryParameters["page_size"] ?? Maybe<string>.None;
        var page = queryParameters["page"] ?? Maybe<string>.None;
        return new QueryParameters(pageSize.Map(int.Parse), page.Map(int.Parse));
    }

    private record QueryParameters(
        Maybe<int> PageSize,
        Maybe<int> Page)
    {
        public IBuilderForOptional ApplyPageSize(IBuilderForOptional builder) =>
            this.PageSize.Match(builder.WithPageSize, () => builder);

        public IBuilderForOptional ApplyPage(IBuilderForOptional builder) =>
            this.Page.Match(builder.WithPage, () => builder);
    }
}