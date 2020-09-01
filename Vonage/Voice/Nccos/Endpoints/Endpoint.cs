using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.Nccos.Endpoints
{
    public abstract class Endpoint
    {
        /// <summary>
        /// the type of endpoint being connected
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EndpointType Type { get; protected set; }

        public enum EndpointType
        {
            phone=1,
            app=2,
            websocket=3,
            sip=4,
            vbc=5
        }
    }
}
