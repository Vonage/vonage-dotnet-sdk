#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Represents a Vonage application. Applications contain the configuration for products such as Voice, Messages,
///     RTC, and Verify.
/// </summary>
public class Application
{
    /// <summary>
    ///     The capabilities enabled for this application, such as Voice, Messages, RTC, or Verify.
    /// </summary>
    [JsonProperty("capabilities")]
    public ApplicationCapabilities Capabilities { get; set; }

    /// <summary>
    ///     The unique identifier for the application.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    ///     The public and private keys for the application. The private key is only returned when the application is created.
    /// </summary>
    [JsonProperty("keys")]
    public Keys Keys { get; set; }

    /// <summary>
    ///     A friendly name for the application. This name is not required to be unique.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Privacy settings for the application, including AI improvement opt-in.
    /// </summary>
    [JsonProperty("privacy")]
    public PrivacySettings Privacy { get; set; }
}