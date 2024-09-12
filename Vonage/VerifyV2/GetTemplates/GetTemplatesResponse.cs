#region
using System;
using System.Text.Json.Serialization;
using System.Web;
using Vonage.Common;
using Vonage.Common.Monads;
#endregion

namespace Vonage.VerifyV2.GetTemplates;

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

public record GetTemplatesEmbedded(
    [property: JsonPropertyName("templates")]
    [property: JsonPropertyOrder(0)]
    Template[] Templates);

/// <summary>
///     Represents a link to another resource.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record GetTemplatesHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms the link into a GetEventsRequest using the cursor pagination.
    /// </summary>
    /// <returns></returns>
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