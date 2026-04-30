using System.Text.Json.Serialization;
using Vonage.ApplicationsNew.Capabilities;

namespace Vonage.ApplicationsNew;

/// <summary>
///     Represents the capabilities enabled on a Vonage application as returned by the API.
/// </summary>
public record ApplicationCapabilities
{
    /// <summary>
    ///     Voice call handling configuration, including answer, event, and fallback webhook URLs.
    /// </summary>
    [JsonPropertyName("voice")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VoiceCapability Voice { get; init; }

    /// <summary>
    ///     Messages (inbound and status) webhook configuration.
    /// </summary>
    [JsonPropertyName("messages")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessagesCapability Messages { get; init; }

    /// <summary>
    ///     RTC / Client SDK event webhook configuration.
    /// </summary>
    [JsonPropertyName("rtc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RtcCapability Rtc { get; init; }

    /// <summary>
    ///     Enables zero-rated VBC calls. Set to a non-null instance to enable; omit to disable.
    /// </summary>
    [JsonPropertyName("vbc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VbcCapability Vbc { get; init; }

    /// <summary>
    ///     Network APIs configuration for network operator integrations.
    /// </summary>
    [JsonPropertyName("network_apis")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NetworkApisCapability NetworkApis { get; init; }

    /// <summary>
    ///     Meetings webhook configuration for recording, room, and session events.
    /// </summary>
    [JsonPropertyName("meetings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MeetingsCapability Meetings { get; init; }

    /// <summary>
    ///     Verify v2 status webhook configuration.
    /// </summary>
    [JsonPropertyName("verify")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VerifyCapability Verify { get; init; }

    /// <summary>
    ///     Video API webhook configuration for session, stream, and archive events.
    /// </summary>
    [JsonPropertyName("video")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoCapability Video { get; init; }

    /// <summary>
    ///     Returns true if at least one capability is configured.
    /// </summary>
    [JsonIgnore]
    public bool HasCapabilities =>
        this.Voice != null ||
        this.Messages != null ||
        this.Rtc != null ||
        this.Vbc != null ||
        this.NetworkApis != null ||
        this.Meetings != null ||
        this.Verify != null ||
        this.Video != null;
}
