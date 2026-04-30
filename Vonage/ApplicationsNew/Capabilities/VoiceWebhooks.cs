using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents the set of webhook endpoints for the Voice capability.
/// </summary>
public record VoiceWebhooks
{
    /// <summary>
    ///     The URL Vonage calls when a call is placed or received. Must return an NCCO.
    /// </summary>
    [JsonPropertyName("answer_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VoiceWebhook AnswerUrl { get; init; }

    /// <summary>
    ///     Fallback URL used when the answer URL is offline or returns an error. Must return an NCCO.
    /// </summary>
    [JsonPropertyName("fallback_answer_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VoiceWebhook FallbackAnswerUrl { get; init; }

    /// <summary>
    ///     The URL Vonage sends call events (ringing, answered, etc.) to.
    /// </summary>
    [JsonPropertyName("event_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VoiceWebhook EventUrl { get; init; }
}
