using Newtonsoft.Json;

namespace Vonage.Voice.Nccos.Endpoints;

public class WebsocketEndpoint : Endpoint
{
    /// <summary>
    /// the internet media type for the audio you are streaming. Possible values are: audio/l16;rate=16000 or audio/l16;rate=8000.
    /// </summary>
    [JsonProperty("content-type")]
    public string ContentType { get; set; }

    /// <summary>
    /// an object containing any metadata you want. See connecting to a websocket for example headers
    /// </summary>
    [JsonProperty("headers")]
    public object Headers { get; set; }

    /// <summary>
    /// the URI to the websocket you are streaming to.
    /// </summary>
    [JsonProperty("uri")]
    public string Uri { get; set; }

    public WebsocketEndpoint()
    {
        this.Type = EndpointType.Websocket;
    }
}