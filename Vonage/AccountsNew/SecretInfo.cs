using System;
using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.AccountsNew;

/// <summary>
///     Represents an API secret associated with a Vonage account.
/// </summary>
public record SecretInfo(
    [property: JsonPropertyName("_links")] HalLinks Links,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("created_at")] DateTimeOffset CreatedAt);
