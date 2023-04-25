using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.ProactiveConnect.Lists;

/// <summary>
///     Represents a list data source.
/// </summary>
public struct ListDataSource
{
    /// <summary>
    ///     Integration id defining salesforce credential to use for this datasource
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonPropertyName("integration_id")]
    public string IntegrationId { get; set; }

    /// <summary>
    ///     Salesforce query defining which data to fetch from salesforce
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("soql")]
    public string Soql { get; set; }

    /// <summary>
    ///     The datasource type.
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<ListDataSourceType>))]
    public ListDataSourceType Type { get; set; }
}

/// <summary>
///     Represents a type of datasource.
/// </summary>
public enum ListDataSourceType
{
    /// <summary>
    ///     Indicates a manual datasource.
    /// </summary>
    [Description("manual")] Manual,

    /// <summary>
    ///     Indicates a salesforce datasource.
    /// </summary>
    [Description("salesforce")] Salesforce,
}