using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;

#pragma warning disable CS1591

namespace Vonage.ProactiveConnect.Lists.GetLists;

public struct EmbeddedLists
{
    public IEnumerable<ListItem> Lists { get; set; }
}

public struct ListItem
{
    [JsonPropertyName("attributes")] public IEnumerable<ListAttribute> Attributes { get; set; }

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("items_count")] public int ItemsCount { get; set; }

    [JsonPropertyName("datasource")] public ListDataSource ListDataSource { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("sync_status")] public SyncStatus SyncStatus { get; set; }

    [JsonPropertyName("tags")] public List<string> Tags { get; set; }

    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
}

public struct SyncStatus
{
    [JsonPropertyName("data_modified")] public bool DataModified { get; set; }

    [JsonPropertyName("dirty")] public bool Dirty { get; set; }

    [JsonPropertyName("metadata_modified")]
    public bool MetadataModified { get; set; }

    [JsonPropertyName("value")] public string Value { get; set; }
}

public struct GetListsResponse
{
    [JsonPropertyName("_embedded")] public EmbeddedLists EmbeddedLists { get; set; }

    [JsonPropertyName("_links")] public HalLinks Links { get; set; }

    [JsonPropertyName("page")] public int Page { get; set; }

    [JsonPropertyName("page_size")] public int PageSize { get; set; }

    [JsonPropertyName("total_items")] public int TotalItems { get; set; }

    [JsonPropertyName("total_pages")] public int TotalPages { get; set; }
}