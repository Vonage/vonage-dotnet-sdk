#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Voice.Emergency.GetAddresses;

/// <summary>
/// </summary>
/// <param name="Page"></param>
/// <param name="PageSize"></param>
/// <param name="TotalPages"></param>
/// <param name="TotalItems"></param>
/// <param name="Embedded"></param>
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
/// </summary>
/// <param name="Addresses"></param>
public record GetAddressesEmbedded(
    [property: JsonPropertyName("addresses")]
    Address[] Addresses);