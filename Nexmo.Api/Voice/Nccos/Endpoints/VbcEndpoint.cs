using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    public class VbcEndpoint : Endpoint
    {
        /// <summary>
        /// the VBC extension to connect the call to.
        /// </summary>
        [JsonProperty("extension")]
        public string Extension { get; set; }

        public VbcEndpoint()
        {
            Type = EndpointType.vbc;
        }
    }
}
