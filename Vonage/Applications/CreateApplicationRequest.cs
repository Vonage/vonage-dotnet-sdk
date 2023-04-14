using Newtonsoft.Json;

namespace Vonage.Applications;

/// <summary>
///     Represents a request to create an application.
/// </summary>
public class CreateApplicationRequest
{
    /// <summary>
    ///     Application Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
        
    /// <summary>
    /// Your application can use multiple products. This contains the configuration for each product. 
    /// This replaces the application type from version 1 of the Application API.
    /// </summary>
    [JsonProperty("capabilities")]
    public ApplicationCapabilities Capabilities { get; set; }

    /// <summary>
    /// The keys for the application
    /// </summary>
    [JsonProperty("keys")]
    public Keys Keys { get; set; }
}