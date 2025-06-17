#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.Nccos.Endpoints;

public class SipEndpoint : Endpoint
{
    public SipEndpoint() => this.Type = EndpointType.Sip;

    /// <summary>
    ///     the SIP URI to the endpoint you are connecting to in the format sip:rebekka@sip.example.com.
    /// </summary>
    [JsonProperty("uri")]
    public string Uri { get; set; }

    /// <summary>
    ///     key => value string pairs containing any metadata you need
    ///     e.g. { "location": "New York City", "occupation": "developer" }
    /// </summary>
    [JsonProperty("headers")]
    public object Headers { get; set; }

    /// <summary>
    ///     Standard SIP INVITE headers. Unlike the headers property, these are not prepended with X-.
    /// </summary>
    [JsonProperty("standard_headers")]
    public StandardHeader StandardHeaders { get; set; }

    /// <summary>
    ///     The user component of the URI. It will be used along the domain property to create the full SIP URI. If you set
    ///     this property, you must also set domain and leave uri unset.
    /// </summary>
    [JsonProperty("user")]
    public string User { get; set; }

    /// <summary>
    ///     The identifier for a trunk created using the dashboard. This must be a successfully provisioned domain using the
    ///     SIP Trunking dashboard or the Programmable SIP API. The URIs provisioned in the trunk will be used along the user
    ///     property to create the full SIP URI. So for example, if the URI in the trunk is: sip.example.com and user is
    ///     example_user, Vonage will send the call to example_user@sip.example.com. If you set this property, you must leave
    ///     uri unset. Note that this property refers to the domain name, not the domain URI.
    /// </summary>
    [JsonProperty("domain")]
    public string Domain { get; set; }

    /// <summary>
    ///     Standard SIP INVITE headers. Unlike the headers property, these are not prepended with X-.
    /// </summary>
    /// <param name="UserToUser">Transmit user-to-user information if supported by the CC / PBX vendor, as per RFC 7433.</param>
    public record StandardHeader(
        [property: JsonProperty("User-to-User")]
        string UserToUser);
}