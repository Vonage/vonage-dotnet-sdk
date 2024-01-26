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
        var parameters = ExtractQueryParameters(this.Href);
        var builder = new GetUserConversationsRequestBuilder(parameters.Cursor)
            .WithUserId(parameters.UserId)
            .WithPageSize(parameters.PageSize)
            .WithOrder(parameters.Order);
        builder = ApplyOptionalOrderBy(parameters, builder);
        builder = ApplyOptionalStartDate(parameters, builder);
        builder = ApplyOptionalState(parameters, builder);
        builder = ApplyOptionalIncludeCustomData(parameters, builder);
        return builder.Create();
    }

    private static IBuilderForOptional
        ApplyOptionalIncludeCustomData(QueryParameters parameters, IBuilderForOptional builder) =>
        parameters.IncludeCustomData.IfNone(false) ? builder.IncludeCustomData() : builder;

    private static IBuilderForOptional ApplyOptionalState(QueryParameters parameters, IBuilderForOptional builder) =>
        parameters.State.Match(builder.WithState, () => builder);

    private static IBuilderForOptional
        ApplyOptionalStartDate(QueryParameters parameters, IBuilderForOptional builder) =>
        parameters.StartDate.Match(builder.WithStartDate, () => builder);

    private static IBuilderForOptional ApplyOptionalOrderBy(QueryParameters parameters, IBuilderForOptional builder) =>
        parameters.OrderBy.Match(builder.WithOrderBy, () => builder);

    private static QueryParameters ExtractQueryParameters(Uri uri)
    {
        var queryParameters = HttpUtility.ParseQueryString(uri.Query);
        var userId = uri.AbsolutePath.Replace("/v1/users/", string.Empty).Replace("/conversations", string.Empty);
        var startDate = queryParameters["date_start"] ?? Maybe<string>.None;
        var includeCustomData = queryParameters["include_custom_data"] ?? Maybe<string>.None;
        var state = queryParameters["state"] ?? Maybe<string>.None;
        return new QueryParameters(
            userId,
            queryParameters["cursor"],
            int.Parse(queryParameters["page_size"]),
            Enums.Parse<FetchOrder>(queryParameters["order"], false, EnumFormat.Description),
            queryParameters["order_by"],
            startDate.Map(value => DateTimeOffset.Parse(value, CultureInfo.InvariantCulture)),
            includeCustomData.Match(bool.Parse, () => false),
            state.Map(value => Enums.Parse<State>(value, false, EnumFormat.Description)));
    }

    private record QueryParameters(
        string UserId,
        Maybe<string> Cursor,
        int PageSize,
        FetchOrder Order,
        Maybe<string> OrderBy,
        Maybe<DateTimeOffset> StartDate,
        Maybe<bool> IncludeCustomData,
        Maybe<State> State);
}