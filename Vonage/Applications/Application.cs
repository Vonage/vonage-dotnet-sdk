using Newtonsoft.Json;

namespace Vonage.Applications;

public class Application
{
    /// <summary>
    /// Configuration for the products available in this application
    /// </summary>
    [JsonProperty("capabilities")]
    public ApplicationCapabilities Capabilities { get; set; }

    /// <summary>
    /// The application's ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// The keys for your application
    /// </summary>
    [JsonProperty("keys")]
    public Keys Keys { get; set; }

    /// <summary>
    /// Friendly identifier for your application. This is not unique
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
}