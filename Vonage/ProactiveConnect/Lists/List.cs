using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vonage.ProactiveConnect.Lists;

/// <summary>
///     Represents a response when creating a list.
/// </summary>
public struct List
{
    /// <summary>
    ///     Attributes of the list.
    /// </summary>
    public IEnumerable<ListAttribute> Attributes { get; set; }

    /// <summary>
    ///     Date when the list was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     The data source.
    /// </summary>
    [JsonPropertyName("datasource")]
    public ListDataSource DataSource { get; set; }

    /// <summary>
    ///     The description of the resource.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     The list id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     The number of items.
    /// </summary>
    public int ItemsCount { get; set; }

    /// <summary>
    ///     The name of the resource.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Synchronization status between the list content (items) and it's datasource
    /// </summary>
    [JsonPropertyName("sync_status")]
    public SyncStatus SyncStatus { get; set; }

    /// <summary>
    ///     Custom strings assigned with a resource - the request allows up to 10 tags, each must be between 1 and 15
    ///     characters.
    /// </summary>
    public IEnumerable<string> Tags { get; set; }

    /// <summary>
    ///     Date when the list was updated.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}