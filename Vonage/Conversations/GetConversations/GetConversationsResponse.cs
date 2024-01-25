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
        var queryParameters = HttpUtility.ParseQueryString(this.Href.Query);
        var pageSize = queryParameters["page_size"];
        var order = queryParameters["order"];
        var cursor = queryParameters["cursor"] ?? Maybe<string>.None;
        var startDate = queryParameters["date_start"] ?? Maybe<string>.None;
        var endDate = queryParameters["date_end"] ?? Maybe<string>.None;
        var builder = new GetConversationsRequestBuilder(cursor)
            .WithOrder(Enums.Parse<FetchOrder>(order, false, EnumFormat.Description))
            .WithPageSize(int.Parse(pageSize));
        startDate.Map(value => DateTimeOffset.Parse(value, CultureInfo.InvariantCulture))
            .IfSome(value => builder = builder.WithStartDate(value));
        endDate.Map(value => DateTimeOffset.Parse(value, CultureInfo.InvariantCulture))
            .IfSome(value => builder = builder.WithEndDate(value));
        return builder.Create();
    }
}