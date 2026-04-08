#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Voice.Emergency.GetAddresses;

/// <summary>
///     Represents a paginated response containing emergency addresses.
/// </summary>
/// <param name="Page">The current page number (1-based).</param>
/// <param name="PageSize">The number of addresses returned per page.</param>
/// <param name="TotalPages">The total number of pages available.</param>
/// <param name="TotalItems">The total number of addresses across all pages.</param>
/// <param name="Embedded">The embedded collection of addresses for the current page.</param>
public record GetAddressesResponse(
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("page_size")]
    int PageSize,
    [property: JsonPropertyName("total_pages")]
    int TotalPages,
    [property: JsonPropertyName("total_items")]
    int TotalItems,
    [property: JsonPropertyName("_embedded")]
    GetAddressesEmbedded Embedded);

/// <summary>
///     Contains the embedded list of addresses in a paginated response.
/// </summary>
/// <param name="Addresses">The array of emergency addresses.</param>
public record GetAddressesEmbedded(
    [property: JsonPropertyName("addresses")]
    Address[] Addresses);