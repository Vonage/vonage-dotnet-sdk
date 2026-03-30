#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Represents a paginated response containing a list of applications.
/// </summary>
public class ApplicationPage
{
    /// <summary>
    ///     The embedded list of applications matching the request filters.
    /// </summary>
    [JsonProperty("_embedded")]
    public ApplicationList Embedded { get; set; }

    /// <summary>
    ///     The current page number (starts at 1).
    /// </summary>
    [JsonProperty("page")]
    public int? Page { get; set; }

    /// <summary>
    ///     The number of applications returned per page.
    /// </summary>
    [JsonProperty("page_size")]
    public int? PageSize { get; set; }

    /// <summary>
    ///     The total number of applications.
    /// </summary>
    [JsonProperty("total_items")]
    public int? TotalItems { get; set; }

    /// <summary>
    ///     The total number of pages available.
    /// </summary>
    [JsonProperty("total_pages")]
    public int? TotalPages { get; set; }
}