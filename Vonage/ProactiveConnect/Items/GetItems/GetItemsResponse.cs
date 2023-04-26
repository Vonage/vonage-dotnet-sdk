using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.ProactiveConnect.Items.GetItems;

/// <summary>
///     Represents a wrapper for items.
/// </summary>
public struct EmbeddedItems
{
    /// <summary>
    ///     The retrieved items.
    /// </summary>
    public IEnumerable<ListItem> Items { get; set; }
}

/// <summary>
///     Represents a response for a GetItemsResponse request.
/// </summary>
public struct GetItemsResponse
{
    /// <summary>
    ///     The embedded items.
    /// </summary>
    [JsonPropertyName("_embedded")]
    public EmbeddedItems EmbeddedItems { get; set; }

    /// <summary>
    ///     The HAL links.
    /// </summary>
    [JsonPropertyName("_links")]
    public HalLinks Links { get; set; }

    /// <summary>
    ///     The page number.
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    ///     The page size.
    /// </summary>
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    /// <summary>
    ///     The number of total items.
    /// </summary>
    [JsonPropertyName("total_items")]
    public int TotalItems { get; set; }

    /// <summary>
    ///     The number of total pages.
    /// </summary>
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
}