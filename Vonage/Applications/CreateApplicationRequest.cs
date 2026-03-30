#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Represents a request to create an application.
/// </summary>
public class CreateApplicationRequest
{
    /// <summary>
    ///     The capabilities configuration for each product (Voice, Messages, RTC, etc.). This replaces the application type
    ///     from version 1 of the Application API.
    /// </summary>
    [JsonProperty("capabilities", Order = 1)]
    public ApplicationCapabilities Capabilities { get; set; }

    /// <summary>
    ///     The public key for the application. Used for JWT authentication.
    /// </summary>
    [JsonProperty("keys", Order = 2)]
    public Keys Keys { get; set; }

    /// <summary>
    ///     The application name. This is a friendly identifier and does not need to be unique.
    /// </summary>
    [JsonProperty("name", Order = 0)]
    public string Name { get; set; }

    /// <summary>
    ///     Privacy settings for the application, including whether Vonage may use content for AI improvement.
    /// </summary>
    [JsonProperty("privacy", Order = 3)]
    public PrivacySettings Privacy { get; set; }
}