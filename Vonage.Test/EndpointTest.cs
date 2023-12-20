using Newtonsoft.Json;
using Vonage.Serialization;
using Vonage.Voice;
using Vonage.Voice.Nccos.Endpoints;
using Xunit;

namespace Vonage.Test
{
    public class EndpointTest
    {
        [Fact]
        public void TestWebhookEndpoint()
        {
            var expected =
                "{\"type\":\"websocket\",\"uri\":\"wss://www.example.com/ws\",\"content-type\":\"audio/l16;rate=16000\",\"headers\":{\"Bar\":\"bar\"}}";
            var websocketEndpoint = new CallEndpoint
            {
                Type = "websocket",
                Uri = "wss://www.example.com/ws",
                Headers = new Foo {Bar = "bar"},
                ContentType = "audio/l16;rate=16000",
            };
            var json = JsonConvert.SerializeObject(websocketEndpoint, VonageSerialization.SerializerSettings);
            Assert.Equal(expected, json);
        }

        [Fact]
        public void TestNccoEndpoint()
        {
            var expected =
                "{\"uri\":\"wss://www.example.com/ws\",\"content-type\":\"audio/l16;rate=16000\",\"headers\":{\"Bar\":\"bar\"},\"type\":\"websocket\"}";
            var websocketEndpoint = new WebsocketEndpoint
            {
                Uri = "wss://www.example.com/ws",
                Headers = new Foo {Bar = "bar"},
                ContentType = "audio/l16;rate=16000",
            };
            var json = JsonConvert.SerializeObject(websocketEndpoint, VonageSerialization.SerializerSettings);
            Assert.Equal(expected, json);
        }

        public class Foo
        {
            public string Bar { get; set; }
        }
    }
}