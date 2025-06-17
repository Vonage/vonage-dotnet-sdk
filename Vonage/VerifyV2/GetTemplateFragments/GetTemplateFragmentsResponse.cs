#region
using System;
using System.Text.Json.Serialization;
using System.Web;
using Vonage.Common;
using Vonage.Common.Monads;
#endregion

namespace Vonage.VerifyV2.GetTemplateFragments;

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

public record GetTemplateFragmentsEmbedded(
    [property: JsonPropertyName("template_fragments")]
    [property: JsonPropertyOrder(0)]
    TemplateFragment[] Fragments);

/// <summary>
///     Represents a link to another resource.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record GetTemplateFragmentsHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms the link into a GetEventsRequest using the cursor pagination.
    /// </summary>
    /// <returns></returns>
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