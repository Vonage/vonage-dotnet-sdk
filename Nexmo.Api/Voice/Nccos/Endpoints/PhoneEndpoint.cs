using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos.Endpoints
{
    public class PhoneEndpoint : Endpoint
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("dtmfAnswer")]
        public string DtmfAnswer { get; set; }

        [JsonProperty("onAnswer")]
        public string OnAnswer { get; set; }

        public PhoneEndpoint()
        {
            Type = EndpointType.phone;
        }
        
    }
}
