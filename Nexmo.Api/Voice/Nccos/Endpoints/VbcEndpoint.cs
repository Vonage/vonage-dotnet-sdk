using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    public class VbcEndpoint : Endpoint
    {
        [JsonProperty("extension")]
        public string Extension { get; set; }

        public VbcEndpoint()
        {
            Type = EndpointType.vbc;
        }
    }
}
