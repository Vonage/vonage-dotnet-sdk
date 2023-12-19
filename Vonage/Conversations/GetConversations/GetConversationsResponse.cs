using System;
using System.Text.Json.Serialization;
using System.Web;
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
        var name = queryParameters["name"] ?? Maybe<string>.None;
        var cursor = queryParameters["cursor"] ?? Maybe<string>.None;
        var pageSize = queryParameters["page_size"];
        var order = queryParameters["order"];
        if (pageSize is null)
        {
            //return Result<GetConversationsRequest>.FromFailure(ResultFailure.FromErrorMessage("PageSize is missing from Uri."));
        }

        if (order is null)
        {
            //return Result<GetConversationsRequest>.FromFailure(ResultFailure.FromErrorMessage("Order is missing from Uri."));
        }

        //return Result<GetConversationsRequest>.FromSuccess(new GetUsersRequest(cursor, name,
        //  Enums.Parse<FetchOrder>(order, false, EnumFormat.Description), int.Parse(pageSize)));
        throw new NotImplementedException();
    }
}