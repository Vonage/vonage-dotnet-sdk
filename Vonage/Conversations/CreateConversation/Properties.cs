using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vonage.Conversations.CreateConversation;

/// <summary>
///     Represents the Conversation Properties.
/// </summary>
/// <param name="TimeToLive">Conversation time to live. After how many seconds an empty conversation is deleted.</param>
/// <param name="Type">The conversation type.</param>
/// <param name="CustomSortKey">The custom sort key.</param>
/// <param name="CustomData">The custom data.</param>
public record Properties(
    [property: JsonPropertyName("ttl")]
    [property: JsonPropertyOrder(0)]
    int TimeToLive,
    [property: JsonPropertyName("type")]
    [property: JsonPropertyOrder(1)]
    string Type,
    [property: JsonPropertyName("custom_sort_key")]
    [property: JsonPropertyOrder(2)]
    string CustomSortKey,
    [property: JsonPropertyName("custom_data")]
    [property: JsonPropertyOrder(3)]
    Dictionary<string, string> CustomData);