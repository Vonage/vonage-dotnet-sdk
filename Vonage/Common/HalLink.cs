#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a set of HAL (Hypertext Application Language) navigation links for paginated API responses.
/// </summary>
/// <remarks>
///     <para>HAL links provide a standardized way to navigate between pages of results.</para>
///     <para>Not all links will be present in every response - for example, <see cref="Next"/> will be default on the last page.</para>
/// </remarks>
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
///     Represents a set of typed HAL (Hypertext Application Language) navigation links for paginated API responses.
/// </summary>
/// <typeparam name="T">The type of the link object.</typeparam>
/// <remarks>
///     This generic version allows for strongly-typed link objects beyond the standard <see cref="HalLink"/> type.
/// </remarks>
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
///     Represents a HAL (Hypertext Application Language) link to a related resource or page.
/// </summary>
/// <param name="Href">The URI of the linked resource.</param>
public record HalLink(
    [property: JsonPropertyName("href")]
    [property: JsonProperty("href")]
    Uri Href);