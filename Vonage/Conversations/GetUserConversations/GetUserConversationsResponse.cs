using System;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Web;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Conversations.GetConversations;

namespace Vonage.Conversations.GetUserConversations;

public record GetUserConversationsResponse(
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
        builder = parameters.ApplyOptionalOrderBy(builder);
        builder = parameters.ApplyOptionalStartDate(builder);
        builder = parameters.ApplyOptionalState(builder);
        builder = parameters.ApplyOptionalIncludeCustomData(builder);
        return builder.Create();
    }

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
        Maybe<State> State)
    {
        public IBuilderForOptional ApplyOptionalIncludeCustomData(IBuilderForOptional builder) =>
            this.IncludeCustomData.IfNone(false) ? builder.IncludeCustomData() : builder;

        public IBuilderForOptional ApplyOptionalState(IBuilderForOptional builder) =>
            this.State.Match(builder.WithState, () => builder);

        public IBuilderForOptional ApplyOptionalStartDate(IBuilderForOptional builder) =>
            this.StartDate.Match(builder.WithStartDate, () => builder);

        public IBuilderForOptional ApplyOptionalOrderBy(IBuilderForOptional builder) =>
            this.OrderBy.Match(builder.WithOrderBy, () => builder);
    }
}