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
        public Answer OnAnswer { get; set; }

        public PhoneEndpoint()
        {
            Type = EndpointType.phone;
        }
        
        public class Answer
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("ringbackTone")]
            public string RingbackTone { get; set; }
        }
    }
}
