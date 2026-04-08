using Newtonsoft.Json;

namespace Vonage.Voice.Nccos.Endpoints;

/// <summary>
///     Represents a Vonage Business Communications (VBC) endpoint for connecting a call to a VBC extension.
/// </summary>
public class VbcEndpoint : Endpoint
{
    /// <summary>
    ///     The VBC extension number to connect the call to.
    /// </summary>
    [JsonProperty("extension")]
    public string Extension { get; set; }

    public VbcEndpoint()
    {
        this.Type = EndpointType.Vbc;
    }
}