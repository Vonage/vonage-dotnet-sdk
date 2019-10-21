using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    public class AppEndpoint : Endpoint
    {
        [JsonProperty("user")]
        public string User { get; set; }

        public AppEndpoint()
        {
            Type = EndpointType.app;
        }
    }
}
