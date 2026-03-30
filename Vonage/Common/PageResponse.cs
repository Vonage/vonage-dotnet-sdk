#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a paginated API response following the HAL (Hypertext Application Language) standard.
/// </summary>
/// <typeparam name="T">The type of the embedded content (typically a collection wrapper).</typeparam>
/// <remarks>
///     <para>HAL responses include navigation links for pagination and embedded content.</para>
///     <para>Use the <see cref="Links" /> property to navigate between pages.</para>
/// </remarks>
public class PageResponse<T> where T : class
{
    /// <summary>
    ///     Gets or sets the total number of records across all pages.
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    ///     Gets or sets the number of records per page.
    /// </summary>
    [JsonProperty("page_size")]
    public int PageSize { get; set; }

    /// <summary>
    ///     Gets or sets the current page index (zero-based).
    /// </summary>
    [JsonProperty("page_index")]
    public int PageIndex { get; set; }

    /// <summary>
    ///     Gets or sets the HAL navigation links for pagination (self, next, previous, first, last).
    /// </summary>
    [JsonProperty("_links")]
    public HALLinks Links { get; set; }

    /// <summary>
    ///     Gets or sets the embedded content containing the actual data records.
    /// </summary>
    [JsonProperty("_embedded")]
    public T Embedded { get; set; }
}