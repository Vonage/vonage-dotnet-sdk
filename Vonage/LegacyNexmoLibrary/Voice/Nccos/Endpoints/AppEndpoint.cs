using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    [System.Obsolete("The Nexmo.Api.Voice.Nccos.Endpoints.AppEndpoint class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.Nccos.Endpoints.AppEndpoint class.")]
    public class AppEndpoint : Endpoint
    {
        /// <summary>
        /// 	the username of the user to connect to. This username must have been added as a user
        /// </summary>
        [JsonProperty("user")]
        public string User { get; set; }

        public AppEndpoint()
        {
            Type = EndpointType.app;
        }
    }
}
