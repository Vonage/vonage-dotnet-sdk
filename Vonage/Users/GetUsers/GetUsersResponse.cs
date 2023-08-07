using System;
using System.Text.Json.Serialization;
using EnumsNET;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect;

namespace Vonage.Users.GetUsers;

public record GetUsersResponse(
    [property: JsonPropertyOrder(0)] int PageSize,
    [property: JsonPropertyName("_embedded")]
    [property: JsonPropertyOrder(1)]
    EmbeddedUsers Embedded,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(2)]
    HalLinks<GetUsersHalLink> Links);

/// <summary>
///     Represents a link to another resource.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record GetUsersHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms the link into a GetUsersRequest using the cursor pagination.
    /// </summary>
    /// <returns></returns>
    public Result<GetUsersRequest> BuildRequest()
    {
        var queryValues = QueryHelpers.ParseQuery(this.Href.Query);
        var name = queryValues.TryGetValue("name", out var value)
            ? Maybe<string>.Some(value.ToString())
            : Maybe<string>.None;
        if (!queryValues.ContainsKey("page_size"))
        {
            return Result<GetUsersRequest>.FromFailure(ResultFailure.FromErrorMessage("PageSize is missing from Uri."));
        }

        if (!queryValues.ContainsKey("order"))
        {
            return Result<GetUsersRequest>.FromFailure(ResultFailure.FromErrorMessage("Order is missing from Uri."));
        }

        if (!queryValues.ContainsKey("cursor"))
        {
            return Result<GetUsersRequest>.FromFailure(ResultFailure.FromErrorMessage("Cursor is missing from Uri."));
        }

        return Result<GetUsersRequest>.FromSuccess(new GetUsersRequest(queryValues["cursor"].ToString(), name,
            Enums.Parse<FetchOrder>(queryValues["order"].ToString(), false, EnumFormat.Description),
            int.Parse(queryValues["page_size"].ToString())));
    }
}

public record EmbeddedUsers(UserSummary[] Users);

public record UserSummary(
    [property: JsonPropertyOrder(0)] string Id,
    [property: JsonPropertyName("name")]
    [property: JsonPropertyOrder(1)]
    string Name,
    [property: JsonPropertyName("display_name")]
    [property: JsonPropertyOrder(2)]
    string DisplayName,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(3)]
    HalLinks Links);