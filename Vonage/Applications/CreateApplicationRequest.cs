using Newtonsoft.Json;

namespace Vonage.Applications;

/// <summary>
///     Represents a request to create an application.
/// </summary>
public class CreateApplicationRequest
{
    /// <summary>
    ///     Your application can use multiple products. This contains the configuration for each product.
    ///     This replaces the application type from version 1 of the Application API.
    /// </summary>
    [JsonProperty("capabilities", Order = 1)]
    public ApplicationCapabilities Capabilities { get; set; }

    /// <summary>
    ///     The keys for the application
    /// </summary>
    [JsonProperty("keys", Order = 2)]
    public Keys Keys { get; set; }

    /// <summary>
    ///     Application Name
    /// </summary>
    [JsonProperty("name", Order = 0)]
    public string Name { get; set; }

    /// <summary>
    ///     Application privacy config
    /// </summary>
    [JsonProperty("privacy", Order = 3)]
    public PrivacySettings Privacy { get; set; }
}