#region
using System;
using System.Text.Json.Serialization;
using System.Web;
using Vonage.Common;
using Vonage.Common.Monads;
#endregion

namespace Vonage.VerifyV2.GetTemplateFragments;

/// <summary>
///     Represents a paginated response containing a list of template fragments for a specific template.
/// </summary>
/// <param name="PageSize">The number of fragments returned per page.</param>
/// <param name="Page">The current page number (1-based index).</param>
/// <param name="TotalPages">The total number of pages available.</param>
/// <param name="TotalItems">The total number of template fragments across all pages.</param>
/// <param name="Embedded">The embedded collection containing the template fragments array.</param>
/// <param name="Links">HAL navigation links for pagination (self, next, prev, first, last).</param>
public record GetTemplateFragmentsResponse(
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
    GetTemplateFragmentsEmbedded Embedded,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(5)]
    HalLinks<GetTemplateFragmentsHalLink> Links);

/// <summary>
///     Represents the embedded container holding the template fragments array in a HAL response.
/// </summary>
/// <param name="Fragments">The array of template fragments in the current page.</param>
public record GetTemplateFragmentsEmbedded(
    [property: JsonPropertyName("template_fragments")]
    [property: JsonPropertyOrder(0)]
    TemplateFragment[] Fragments);

/// <summary>
///     Represents a HAL navigation link for template fragment pagination.
/// </summary>
/// <param name="Href">The URL pointing to a page of template fragments.</param>
public record GetTemplateFragmentsHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms this navigation link into a <see cref="GetTemplateFragmentsRequest"/> for fetching the linked page.
    /// </summary>
    /// <returns>A <see cref="Result{T}"/> containing the request with pagination parameters extracted from the link URL.</returns>
    public Result<GetTemplateFragmentsRequest> BuildRequest()
    {
        var parameters = ExtractQueryParameters(this.Href);
        var builder = new GetTemplateFragmentsRequestBuilder().WithTemplateId(parameters.TemplateId);
        builder = parameters.ApplyPageSize(builder);
        builder = parameters.ApplyPage(builder);
        return builder.Create();
    }

    private static QueryParameters ExtractQueryParameters(Uri uri)
    {
        var queryParameters = HttpUtility.ParseQueryString(uri.Query);
        var templateId = Guid.Parse(uri.Segments[^2].Trim('/'));
        var pageSize = queryParameters["page_size"] ?? Maybe<string>.None;
        var page = queryParameters["page"] ?? Maybe<string>.None;
        return new QueryParameters(templateId, pageSize.Map(int.Parse), page.Map(int.Parse));
    }

    private record QueryParameters(
        Guid TemplateId,
        Maybe<int> PageSize,
        Maybe<int> Page)
    {
        public IBuilderForOptional ApplyPageSize(IBuilderForOptional builder) =>
            this.PageSize.Match(builder.WithPageSize, () => builder);

        public IBuilderForOptional ApplyPage(IBuilderForOptional builder) =>
            this.Page.Match(builder.WithPage, () => builder);
    }
}