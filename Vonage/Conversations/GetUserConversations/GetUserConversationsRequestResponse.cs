using System;
using System.Globalization;
using System.Web;
using EnumsNET;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetUserConversations;

public record GetUserConversationsRequestResponse;

/// <summary>
///     Represents a link to another resource.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record GetUserConversationsHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms the link into a GetUserConversationsRequest using the cursor pagination.
    /// </summary>
    /// <returns></returns>
    public Result<GetUserConversationsRequest> BuildRequest()
    {
        var queryParameters = HttpUtility.ParseQueryString(this.Href.Query);
        var userId = this.Href.AbsolutePath.Replace("/v1/users/", string.Empty).Replace("/conversations", string.Empty);
        var pageSize = queryParameters["page_size"];
        var order = queryParameters["order"];
        var orderBy = queryParameters["order_by"] ?? Maybe<string>.None;
        var cursor = queryParameters["cursor"] ?? Maybe<string>.None;
        var startDate = queryParameters["date_start"] ?? Maybe<string>.None;
        var includeCustomData = queryParameters["include_custom_data"] ?? Maybe<string>.None;
        var state = queryParameters["state"] ?? Maybe<string>.None;
        return Result<GetUserConversationsRequest>.FromSuccess(new GetUserConversationsRequest
        {
            Cursor = cursor,
            IncludeCustomData = includeCustomData.Match(bool.Parse, () => false),
            Order = Enums.Parse<FetchOrder>(order, false, EnumFormat.Description),
            OrderBy = orderBy.IfNone(GetUserConversationsRequestBuilder.DefaultOrderBy),
            PageSize = int.Parse(pageSize),
            StartDate = startDate.Map(value => DateTimeOffset.Parse(value, CultureInfo.InvariantCulture)),
            State = state.Map(value => Enums.Parse<State>(value, false, EnumFormat.Description)),
            UserId = userId,
        });
    }
}