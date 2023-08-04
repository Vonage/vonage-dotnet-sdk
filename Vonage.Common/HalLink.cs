using System;
using System.Text.Json.Serialization;

namespace Vonage.Common;

/// <summary>
///     Represents a set of HAL Links.
/// </summary>
public struct HalLinks
{
    /// <summary>
    ///     Represents the navigation link to the first element.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink First { get; set; }

    /// <summary>
    ///     Represents the navigation link to the last element.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Last { get; set; }

    /// <summary>
    ///     Represents the navigation link to the next element.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Next { get; set; }

    /// <summary>
    ///     Represents the navigation link to the previous element.
    /// </summary>
    [JsonPropertyName("prev")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Previous { get; set; }

    /// <summary>
    ///     Represents the navigation link to the current element.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Self { get; set; }
}

/// <summary>
///     Represents a link to another page.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record HalLink(Uri Href);