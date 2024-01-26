using System;
using System.Text.Json.Serialization;
using System.Web;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect;

namespace Vonage.Users.GetUsers;

/// <summary>
/// </summary>
/// <param name="PageSize">The amount of records returned in this response.</param>
/// <param name="Embedded">
///     A list of user objects. See
///     <see href="https://developer.vonage.com/en/api/application.v2#getUser">the get details</see> of a specific user
///     response fields for a description of the nested objects
/// </param>
/// <param name="Links">A series of links between resources in this API in the http://stateless.co/hal_specification.html.</param>
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
        var queryParameters = HttpUtility.ParseQueryString(this.Href.Query);
        var name = queryParameters["name"] ?? Maybe<string>.None;
        var cursor = queryParameters["cursor"];
        var pageSize = queryParameters["page_size"];
        var order = queryParameters["order"];
        if (pageSize is null)
        {
            return Result<GetUsersRequest>.FromFailure(ResultFailure.FromErrorMessage("PageSize is missing from Uri."));
        }

        if (order is null)
        {
            return Result<GetUsersRequest>.FromFailure(ResultFailure.FromErrorMessage("Order is missing from Uri."));
        }

        var builder = new GetUsersRequestBuilder(cursor)
            .WithPageSize(int.Parse(pageSize))
            .WithOrder(Enums.Parse<FetchOrder>(order, false, EnumFormat.Description));
        name.IfSome(value => builder.WithName(value));
        return builder.Create();
    }
}

/// <summary>
///     Represents a list of user objects.
/// </summary>
/// <param name="Users">List of users matching the provided filter.</param>
public record EmbeddedUsers(UserSummary[] Users);

/// <summary>
///     Represents a user summary.
/// </summary>
/// <param name="Id">User ID</param>
/// <param name="Name">Unique name for a user</param>
/// <param name="DisplayName">A string to be displayed as user name. It does not need to be unique</param>
/// <param name="Links">A series of links between resources in this API in the http://stateless.co/hal_specification.html.</param>
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