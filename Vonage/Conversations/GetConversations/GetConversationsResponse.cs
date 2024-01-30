using System;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Web;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetConversations;

/// <summary>
/// </summary>
/// <param name="PageSize"></param>
/// <param name="Embedded"></param>
/// <param name="Links"></param>
public record GetConversationsResponse(
    [property: JsonPropertyName("page_size")]
    [property: JsonPropertyOrder(0)]
    int PageSize,
    [property: JsonPropertyName("_embedded")]
    [property: JsonPropertyOrder(1)]
    EmbeddedConversations Embedded,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(2)]
    HalLinks<GetConversationsHalLink> Links);

/// <summary>
///     Represents a list of conversations.
/// </summary>
/// <param name="Conversations">List of conversations matching the provided filter.</param>
public record EmbeddedConversations(Conversation[] Conversations);

/// <summary>
///     Represents a link to another resource.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record GetConversationsHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms the link into a GetUsersRequest using the cursor pagination.
    /// </summary>
    /// <returns></returns>
    public Result<GetConversationsRequest> BuildRequest()
    {
        var parameters = ExtractQueryParameters(this.Href);
        var builder = new GetConversationsRequestBuilder(parameters.Cursor)
            .WithPageSize(parameters.PageSize)
            .WithOrder(parameters.Order);
        builder = ApplyOptionalStartDate(parameters, builder);
        builder = ApplyOptionalEndDate(parameters, builder);
        return builder.Create();
    }

    private static IBuilderForOptional
        ApplyOptionalStartDate(QueryParameters parameters, IBuilderForOptional builder) =>
        parameters.StartDate.Match(builder.WithStartDate, () => builder);

    private static IBuilderForOptional
        ApplyOptionalEndDate(QueryParameters parameters, IBuilderForOptional builder) =>
        parameters.EndDate.Match(builder.WithEndDate, () => builder);

    private static QueryParameters ExtractQueryParameters(Uri uri)
    {
        var queryParameters = HttpUtility.ParseQueryString(uri.Query);
        var startDate = queryParameters["date_start"] ?? Maybe<string>.None;
        var endDate = queryParameters["date_end"] ?? Maybe<string>.None;
        return new QueryParameters(
            queryParameters["cursor"],
            int.Parse(queryParameters["page_size"]),
            Enums.Parse<FetchOrder>(queryParameters["order"], false, EnumFormat.Description),
            startDate.Map(value => DateTimeOffset.Parse(value, CultureInfo.InvariantCulture)),
            endDate.Map(value => DateTimeOffset.Parse(value, CultureInfo.InvariantCulture)));
    }

    private record QueryParameters(
        Maybe<string> Cursor,
        int PageSize,
        FetchOrder Order,
        Maybe<DateTimeOffset> StartDate,
        Maybe<DateTimeOffset> EndDate);
}