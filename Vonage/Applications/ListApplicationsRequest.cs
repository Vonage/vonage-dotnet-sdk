#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Represents a request to list applications with pagination parameters.
/// </summary>
public class ListApplicationsRequest
{
    /// <summary>
    ///     The number of applications to return per page.
    /// </summary>
    [JsonProperty("page_size")]
    public int PageSize { get; set; }

    /// <summary>
    ///     The page number to retrieve (starts at 1).
    /// </summary>
    [JsonProperty("page")]
    public int Page { get; set; }
}