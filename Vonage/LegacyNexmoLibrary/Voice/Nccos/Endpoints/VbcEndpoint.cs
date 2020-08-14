using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    [System.Obsolete("The Nexmo.Api.Voice.Nccos.Endpoints.VbcEndpoint class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.Nccos.Endpoints.VbcEndpoint class.")]
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
