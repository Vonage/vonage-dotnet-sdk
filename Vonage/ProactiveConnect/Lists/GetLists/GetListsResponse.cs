using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.ProactiveConnect.Lists.GetLists;

/// <summary>
///     Represents a wrapper for lists.
/// </summary>
public struct EmbeddedLists
{
    /// <summary>
    ///     The retrieved lists.
    /// </summary>
    public IEnumerable<List> Lists { get; set; }
}

/// <summary>
///     Represents a response for a GetLists request.
/// </summary>
public struct GetListsResponse
{
    /// <summary>
    ///     The embedded lists.
    /// </summary>
    [JsonPropertyName("_embedded")]
    public EmbeddedLists EmbeddedLists { get; set; }

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