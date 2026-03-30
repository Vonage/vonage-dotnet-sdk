#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents the standard HAL (Hypertext Application Language) navigation links for paginated responses.
/// </summary>
/// <remarks>
///     <para>HAL links provide a standardized way to navigate between pages of results.</para>
///     <para>
///         Not all links will be present in every response - for example, <see cref="Prev" /> will be null on the first
///         page.
///     </para>
/// </remarks>
public class HALLinks
{
    /// <summary>
    ///     Gets or sets the link to the current page.
    /// </summary>
    [JsonProperty("self")]
    public Link Self { get; set; }

    /// <summary>
    ///     Gets or sets the link to the next page of results. Null if on the last page.
    /// </summary>
    [JsonProperty("next")]
    public Link Next { get; set; }

    /// <summary>
    ///     Gets or sets the link to the previous page of results. Null if on the first page.
    /// </summary>
    [JsonProperty("prev")]
    public Link Prev { get; set; }

    /// <summary>
    ///     Gets or sets the link to the first page of results.
    /// </summary>
    [JsonProperty("first")]
    public Link First { get; set; }

    /// <summary>
    ///     Gets or sets the link to the last page of results.
    /// </summary>
    [JsonProperty("last")]
    public Link Last { get; set; }
}