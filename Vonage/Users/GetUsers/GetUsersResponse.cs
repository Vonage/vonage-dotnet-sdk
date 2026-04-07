using System;
using System.Text.Json.Serialization;
using System.Web;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Users.GetUsers;

/// <summary>
///     Represents the paginated response from retrieving users, containing user summaries and navigation links.
/// </summary>
/// <param name="PageSize">The number of user records returned in this response page.</param>
/// <param name="Embedded">The embedded collection containing the list of user summaries matching the query.</param>
/// <param name="Links">HAL navigation links for pagination, including next and previous page cursors when available.</param>
public record GetUsersResponse(
    [property: JsonPropertyOrder(0)] int PageSize,
    [property: JsonPropertyName("_embedded")]
    [property: JsonPropertyOrder(1)]
    EmbeddedUsers Embedded,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(2)]
    HalLinks<GetUsersHalLink> Links);

/// <summary>
///     Represents a HAL navigation link for paginating through user results.
/// </summary>
/// <param name="Href">The hyperlink reference URL containing pagination parameters.</param>
public record GetUsersHalLink(Uri Href)
{
    /// <summary>
    ///     Transforms this navigation link into a <see cref="GetUsersRequest"/> for fetching the next or previous page of results.
    /// </summary>
    /// <returns>A result containing the <see cref="GetUsersRequest"/> on success, or an error if required parameters are missing from the URL.</returns>
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
///     Represents the embedded collection of user summaries returned in a paginated response.
/// </summary>
/// <param name="Users">The array of user summaries matching the query criteria.</param>
public record EmbeddedUsers(UserSummary[] Users);

/// <summary>
///     Represents a lightweight summary of a user, containing basic identification information without full channel details.
/// </summary>
/// <param name="Id">The unique identifier for the user (e.g., "USR-12345678-1234-1234-1234-123456789012").</param>
/// <param name="Name">The unique name assigned to the user within the Vonage platform.</param>
/// <param name="DisplayName">A human-readable display name for the user. Unlike the Name, this does not need to be unique.</param>
/// <param name="Links">HAL links for navigating to the full user resource.</param>
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