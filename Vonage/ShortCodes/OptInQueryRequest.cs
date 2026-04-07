using Newtonsoft.Json;

namespace Vonage.ShortCodes;

/// <summary>
///     Represents a request to query opt-in records for a short code with pagination support.
/// </summary>
public class OptInQueryRequest
{
    /// <summary>
    ///     Gets or sets the number of records to return per page.
    /// </summary>
    [JsonProperty("page-size")]
    public string PageSize { get; set; }

    /// <summary>
    ///     Gets or sets the page number to retrieve (1-indexed).
    /// </summary>
    [JsonProperty("page")]
    public string Page { get; set; }
}