using System;
using System.Text.Json.Serialization;
using EnumsNET;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common;
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
    HalLinks Links)
{
    public Result<GetUsersRequest> BuildRequestForNext()
    {
        var a = QueryHelpers.ParseQuery(this.Links.Next.Href.Query);
        return Result<GetUsersRequest>.FromSuccess(new GetUsersRequest(a["cursor"].ToString(), a["name"].ToString(),Enums.Parse<FetchOrder>(a["order"].ToString(), false, EnumFormat.Description), int.Parse(a["page_size"].ToString()) ));
    }
};

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