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
    ///     Messages and Dispatch application webhook config
    /// </summary>
    [JsonProperty("messages", Order = 5)]
    public Capabilities.Messages Messages { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("network_apis", Order = 1)]
    public NetworkApis NetworkApis { get; set; }

    /// <summary>
    ///     RTC / Client SDK application webhook config
    /// </summary>
    [JsonProperty("rtc", Order = 2)]
    public Rtc Rtc { get; set; }

    /// <summary>
    ///     Specify vbc capability to enable zero-rated calls for VBC number programmability service applications. This must be
    ///     an empty object.
    /// </summary>
    [JsonProperty("vbc", Order = 3)]
    public Vbc Vbc { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("verify", Order = 6)]
    public Capabilities.Verify Verify { get; set; }

    /// <summary>
    ///     Create in-app video calls with Vonage API.
    /// </summary>
    [JsonProperty("video")]
    public Capabilities.Video Video { get; set; }

    /// <summary>
    ///     Voice application webhook config
    /// </summary>
    [JsonProperty("voice", Order = 0)]
    public Capabilities.Voice Voice { get; set; }
}