using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vonage.Conversations.CreateConversation;

public record Properties(
    [property: JsonPropertyName("ttl")] int TimeToLive, string Type,
    [property: JsonPropertyName("custom_sort_key")]
    string CustomSortKey,
    [property: JsonPropertyName("custom_data")]
    Dictionary<string, string> CustomData);