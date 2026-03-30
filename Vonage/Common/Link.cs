#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a hyperlink reference used in HAL (Hypertext Application Language) responses.
/// </summary>
/// <remarks>
///     This class is typically used within <see cref="HALLinks" /> for pagination navigation.
/// </remarks>
public class Link
{
    /// <summary>
    ///     Gets or sets the URL of the linked resource.
    /// </summary>
    [JsonProperty("href")]
    public string Href { get; set; }
}