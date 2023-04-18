using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vonage.Voice.Nccos.Endpoints;

public class SipEndpoint : Endpoint
{
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

    public SipEndpoint()
    {
        this.Type = EndpointType.Sip;
    }
}