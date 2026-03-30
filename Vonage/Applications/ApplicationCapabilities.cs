#region
using Newtonsoft.Json;
using Vonage.Applications.Capabilities;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Represents capabilities of an application.
/// </summary>
public class ApplicationCapabilities
{
    /// <summary>
    ///     Messages and Dispatch application webhook configuration for inbound messages and status updates.
    /// </summary>
    [JsonProperty("messages", Order = 5)]
    public Capabilities.Messages Messages { get; set; }

    /// <summary>
    ///     Network APIs configuration including network application ID and redirect URI.
    /// </summary>
    [JsonProperty("network_apis", Order = 1)]
    public NetworkApis NetworkApis { get; set; }

    /// <summary>
    ///     RTC / Client SDK application webhook configuration for real-time communication events.
    /// </summary>
    [JsonProperty("rtc", Order = 2)]
    public Rtc Rtc { get; set; }

    /// <summary>
    ///     VBC (Vonage Business Communications) capability to enable zero-rated calls for VBC number programmability
    ///     service applications. This must be an empty object.
    /// </summary>
    [JsonProperty("vbc", Order = 3)]
    public Vbc Vbc { get; set; }

    /// <summary>
    ///     Verify v2 application webhook configuration for verification status updates.
    /// </summary>
    [JsonProperty("verify", Order = 6)]
    public Capabilities.Verify Verify { get; set; }

    /// <summary>
    ///     Video API configuration for in-app video calls, including webhooks for session and stream events.
    /// </summary>
    [JsonProperty("video")]
    public Capabilities.Video Video { get; set; }

    /// <summary>
    ///     Voice application webhook configuration for call events, including answer URL, event URL, and fallback settings.
    /// </summary>
    [JsonProperty("voice", Order = 0)]
    public Capabilities.Voice Voice { get; set; }
}