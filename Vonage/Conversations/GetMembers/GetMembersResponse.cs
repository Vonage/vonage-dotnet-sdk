using System;
using System.Text.Json.Serialization;
using System.Web;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetMembers;

public record GetMembersResponse(
    [property: JsonPropertyName("page_size")]
    [property: JsonPropertyOrder(0)]
    int PageSize,
    [property: JsonPropertyName("_embedded")]
    [property: JsonPropertyOrder(1)]
    EmbeddedMembers Embedded,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(2)]
    HalLinks<GetConversationsHalLink> Links);

/// <summary>
///     Represents a list of conversations.
/// </summary>
/// <param name="Conversations">List of conversations matching the provided filter.</param>
public record EmbeddedMembers(Member[] Members);

/// <summary>
///     Represents a link to another resource.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record GetConversationsHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms the link into a GetMembersRequest using the cursor pagination.
    /// </summary>
    /// <returns></returns>
    public Result<GetMembersRequest> BuildRequest()
    {
        var parameters = ExtractQueryParameters(this.Href);
        return new GetMembersRequestBuilder(parameters.Cursor)
            .WithConversationId(parameters.ConversationId)
            .WithPageSize(parameters.PageSize)
            .WithOrder(parameters.Order).Create();
    }

    private static QueryParameters ExtractQueryParameters(Uri uri)
    {
        var queryParameters = HttpUtility.ParseQueryString(uri.Query);
        return new QueryParameters(
            queryParameters["cursor"],
            ExtractConversationId(uri),
            int.Parse(queryParameters["page_size"]),
            Enums.Parse<FetchOrder>(queryParameters["order"], false, EnumFormat.Description));
    }

    private static string ExtractConversationId(Uri uri) => uri.AbsolutePath.Replace("/v1/conversations/", string.Empty)
        .Replace("/members", string.Empty);

    private record QueryParameters(
        Maybe<string> Cursor,
        string ConversationId,
        int PageSize,
        FetchOrder Order);
}