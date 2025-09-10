#region
using Vonage.Voice;
using Vonage.Voice.Nccos.Endpoints;
#endregion

namespace Vonage.Test.Voice;

internal static class EndpointTestTestData
{
    internal static WebsocketEndpoint CreateWebsocketEndpointWithHeaders() =>
        new WebsocketEndpoint
        {
            Uri = "wss://www.example.com/ws",
            Headers = new EndpointTestHeaders {Bar = "bar"},
            ContentType = "audio/l16;rate=16000",
        };

    internal static CallEndpoint CreateCallEndpointWithHeaders() =>
        new CallEndpoint
        {
            Type = "websocket",
            Uri = "wss://www.example.com/ws",
            Headers = new EndpointTestHeaders {Bar = "bar"},
            ContentType = "audio/l16;rate=16000",
        };
}

internal class EndpointTestHeaders
{
    public string Bar { get; set; }
}