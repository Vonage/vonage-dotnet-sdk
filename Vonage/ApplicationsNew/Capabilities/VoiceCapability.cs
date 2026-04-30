using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents the Voice capability configuration for an application.
/// </summary>
public record VoiceCapability
{
    /// <summary>
    ///     Webhook endpoints for Voice events.
    /// </summary>
    [JsonPropertyName("webhooks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VoiceWebhooks Webhooks { get; init; }

    /// <summary>
    ///     Whether Vonage signs incoming webhook requests so the application can verify their authenticity.
    /// </summary>
    [JsonPropertyName("signed_callbacks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UseSignedCallbacks { get; init; }

    /// <summary>
    ///     How long named conversations stay active after creation, in hours. Maximum 9000 (≈ 365 days).
    /// </summary>
    [JsonPropertyName("conversations_ttl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ConversationsTtl { get; init; }

    /// <summary>
    ///     How long call legs are persisted, in days. Maximum 31.
    /// </summary>
    [JsonPropertyName("leg_persistence_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? LegPersistenceTime { get; init; }

    /// <summary>
    ///     Routes all inbound and programmable SIP calls to the selected region.
    /// </summary>
    [JsonPropertyName("region")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<VoiceRegion>))]
    public VoiceRegion? Region { get; init; }
}
