using Newtonsoft.Json;
using Vonage.Applications.Capabilities;

namespace Vonage.Applications;

/// <summary>
///     Represents capabilities of an application.
/// </summary>
public class ApplicationCapabilities
{
    /// <summary>
    /// Voice application webhook config
    /// </summary>
    [JsonProperty("voice")]
    public Capabilities.Voice Voice { get; set; }

    /// <summary>
    /// RTC / Client SDK application webhook config
    /// </summary>
    [JsonProperty("rtc")]
    public Rtc Rtc { get; set; }

    /// <summary>
    /// Specify vbc capability to enable zero-rated calls for VBC number programmability service applications. This must be an empty object.
    /// </summary>
    [JsonProperty("vbc")]
    public Vbc Vbc { get; set; }
        
    /// <summary>
    ///     Create meetings with Vonage API.
    /// </summary>
    [JsonProperty("meetings")]
    public Capabilities.Meetings Meetings { get; set; }

    /// <summary>
    /// Messages and Dispatch application webhook config
    /// </summary>
    [JsonProperty("messages")]
    public Capabilities.Messages Messages { get; set; }
        
    /// <summary>
    ///    Create in-app video calls with Vonage API.
    /// </summary>
    [JsonProperty("video")]
    public Capabilities.Video Video { get; set; }
}