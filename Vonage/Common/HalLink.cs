#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a set of HAL Links.
/// </summary>
public struct HalLinks
{
    /// <summary>
    ///     Represents the navigation link to the first element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink First { get; set; }

    /// <summary>
    ///     Represents the navigation link to the last element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Last { get; set; }

    /// <summary>
    ///     Represents the navigation link to the next element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Next { get; set; }

    /// <summary>
    ///     Represents the navigation link to the previous element.
    /// </summary>
    [JsonPropertyName("prev")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Previous { get; set; }

    /// <summary>
    ///     Represents the navigation link to the current element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HalLink Self { get; set; }
}

/// <summary>
///     Represents a set of HAL Links.
/// </summary>
public struct HalLinks<T>
{
    /// <summary>
    ///     Represents the navigation link to the first element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T First { get; set; }

    /// <summary>
    ///     Represents the navigation link to the last element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Last { get; set; }

    /// <summary>
    ///     Represents the navigation link to the next element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Next { get; set; }

    /// <summary>
    ///     Represents the navigation link to the previous element.
    /// </summary>
    [JsonPropertyName("prev")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Previous { get; set; }

    /// <summary>
    ///     Represents the navigation link to the current element.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Self { get; set; }
}

/// <summary>
///     Represents a link to another page.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record HalLink(
    [property: JsonPropertyName("href")]
    [property: JsonProperty("href")]
    Uri Href);