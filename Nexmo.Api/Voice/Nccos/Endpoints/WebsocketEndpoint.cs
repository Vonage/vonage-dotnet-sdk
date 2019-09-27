using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    public class WebsocketEndpoint : Endpoint
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("content-type")]
        public string ContentType { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string,string> Headers { get; set; }

        public WebsocketEndpoint()
        {
            Type = EndpointType.websocket;
        }
    }
}
