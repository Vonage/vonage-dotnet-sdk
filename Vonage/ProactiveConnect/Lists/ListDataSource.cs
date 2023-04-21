using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.ProactiveConnect.Lists;

#pragma warning disable CS1591
public struct ListDataSource
{
    [JsonPropertyName("integration_id")] public string IntegrationId { get; set; }

    [JsonPropertyName("soql")] public string Soql { get; set; }

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