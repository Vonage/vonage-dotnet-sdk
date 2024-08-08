#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.Nccos.Endpoints;

public class SipEndpoint : Endpoint
{
    public SipEndpoint() => this.Type = EndpointType.Sip;

    /// <summary>
    /// the SIP URI to the endpoint you are connecting to in the format sip:rebekka@sip.example.com.
    /// </summary>
    [JsonProperty("uri")]
    public string Uri { get; set; }

    /// <summary>
    /// key => value string pairs containing any metadata you need 
    /// e.g. { "location": "New York City", "occupation": "developer" }
    /// </summary>
    [JsonProperty("headers")]
    public object Headers { get; set; }

    /// <summary>
    ///     Standard SIP INVITE headers. Unlike the headers property, these are not prepended with X-.
    /// </summary>
    [JsonProperty("standard_headers")]
    public StandardHeader StandardHeaders { get; set; }

    /// <summary>
    ///     Standard SIP INVITE headers. Unlike the headers property, these are not prepended with X-.
    /// </summary>
    /// <param name="UserToUser">Transmit user-to-user information if supported by the CC / PBX vendor, as per RFC 7433.</param>
    public record StandardHeader(
        [property: JsonProperty("User-to-User")]
        string UserToUser);
}