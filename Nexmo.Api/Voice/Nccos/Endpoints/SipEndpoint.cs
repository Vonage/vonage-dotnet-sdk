using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    public class SipEndpoint : Endpoint
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("headers")]
        public object Headers { get; set; }

        public SipEndpoint()
        {
            Type = EndpointType.sip;
        }
    }
}
