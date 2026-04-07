using Newtonsoft.Json;

namespace Vonage.ShortCodes;

/// <summary>
///     Represents the response from querying opt-in records for a short code.
/// </summary>
public class OptInSearchResponse
{
    /// <summary>
    ///     Gets or sets the total number of opt-in records matching the query.
    /// </summary>
    [JsonProperty("opt-in-count")]
    public string OptInCount { get; set; }

    /// <summary>
    ///     Gets or sets the list of opt-in records. See <see cref="OptInRecord"/> for details on each record.
    /// </summary>
    [JsonProperty("opt-in-list")]
    public OptInRecord[] OptInList { get; set; }
}