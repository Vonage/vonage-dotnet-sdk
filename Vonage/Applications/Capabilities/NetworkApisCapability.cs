using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Network APIs capability configuration for an application.
/// </summary>
public record NetworkApisCapability
{
    /// <summary>
    ///     The network application identifier issued by the network operator.
    /// </summary>
    [JsonPropertyName("network_application_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string NetworkApplicationId { get; init; }

    /// <summary>
    ///     The redirect URI registered with the network operator.
    /// </summary>
    [JsonPropertyName("redirect_uri")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RedirectUri { get; init; }
}
