using Newtonsoft.Json;

namespace Vonage.Voice.Nccos.Endpoints;

/// <summary>
///     Represents an application (Client SDK) endpoint for connecting a call to an in-app user.
/// </summary>
public class AppEndpoint : Endpoint
{
    /// <summary>
    ///     The username of the user to connect to. This user must have been added to the application.
    /// </summary>
    [JsonProperty("user")]
    public string User { get; set; }

    /// <summary>
    ///     Initializes a new <see cref="AppEndpoint"/> with the endpoint type set to <see cref="Endpoint.EndpointType.App"/>.
    /// </summary>
    public AppEndpoint() => this.Type = EndpointType.App;
}