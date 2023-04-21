using System.Text.Json.Serialization;

namespace Vonage.ProactiveConnect.Lists;

/// <summary>
///     Represents an attribute of a list.
/// </summary>
public struct ListAttribute
{
    /// <summary>
    ///     Alternative name to use for this attribute. Use when you wish to correlate between 2 or more list that are using
    ///     different attribute names for the same semantic data
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    /// <summary>
    ///     Set to true if this attribute should be used to correlate between 2 or more lists.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("key")]
    public bool Key { get; set; }

    /// <summary>
    ///     The list attribute name.
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}