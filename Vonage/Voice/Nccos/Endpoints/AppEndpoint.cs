using Newtonsoft.Json;

namespace Vonage.Voice.Nccos.Endpoints
{
    public class AppEndpoint : Endpoint
    {
        /// <summary>
        /// 	the username of the user to connect to. This username must have been added as a user
        /// </summary>
        [JsonProperty("user")]
        public string User { get; set; }

        public AppEndpoint()
        {
            Type = EndpointType.App;
        }
    }
}
