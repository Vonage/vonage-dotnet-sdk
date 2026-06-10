using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the webhook endpoints for the Video capability.
/// </summary>
public record VideoWebhooks
{
    /// <summary>The URL Vonage sends archive status events to.</summary>
    [JsonPropertyName("archive_status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook ArchiveStatus { get; init; }

    /// <summary>The URL Vonage sends connection-created events to.</summary>
    [JsonPropertyName("connection_created")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook ConnectionCreated { get; init; }

    /// <summary>The URL Vonage sends connection-destroyed events to.</summary>
    [JsonPropertyName("connection_destroyed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook ConnectionDestroyed { get; init; }

    /// <summary>The URL Vonage sends session-created events to.</summary>
    [JsonPropertyName("session_created")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook SessionCreated { get; init; }

    /// <summary>The URL Vonage sends session-destroyed events to.</summary>
    [JsonPropertyName("session_destroyed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook SessionDestroyed { get; init; }

    /// <summary>The URL Vonage sends session notifications to.</summary>
    [JsonPropertyName("session_notification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook SessionNotification { get; init; }

    /// <summary>The URL Vonage sends stream-created events to.</summary>
    [JsonPropertyName("stream_created")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook StreamCreated { get; init; }

    /// <summary>The URL Vonage sends stream-destroyed events to.</summary>
    [JsonPropertyName("stream_destroyed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook StreamDestroyed { get; init; }

    /// <summary>The URL Vonage sends render status events to.</summary>
    [JsonPropertyName("render_status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook RenderStatus { get; init; }

    /// <summary>The URL Vonage sends caption status events to.</summary>
    [JsonPropertyName("captions_status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhook CaptionsStatus { get; init; }
}
