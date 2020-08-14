using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Vonage.Voice;
using Vonage.Voice.Nccos.Endpoints;
using Newtonsoft.Json;

namespace Vonage.Test.Unit
{    
    public class EndpointTest
    {

        [Fact]
        public void TestWebhookEndpoint()
        {
            var expected = "{\"type\":\"websocket\",\"uri\":\"wss://www.example.com/ws\",\"content-type\":\"audio/l16;rate=16000\",\"headers\":{\"Bar\":\"bar\"}}";
            var websocketEndpoint = new Call.Endpoint
            {
                type = "websocket",
                uri = "wss://www.example.com/ws",
                headers = new Foo { Bar = "bar" },
                contentType = "audio/l16;rate=16000"
            };
            string json = JsonConvert.SerializeObject(websocketEndpoint,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });    
            Assert.Equal(expected, json);
        }

        [Fact]
        public void TestNccoEndpoint()
        {
            var expected = "{\"uri\":\"wss://www.example.com/ws\",\"content-type\":\"audio/l16;rate=16000\",\"headers\":{\"Bar\":\"bar\"},\"type\":\"websocket\"}";
            var websocketEndpoint = new WebsocketEndpoint
            {                
                Uri = "wss://www.example.com/ws",
                Headers = new Foo { Bar = "bar" },
                ContentType = "audio/l16;rate=16000"
            };
            string json = JsonConvert.SerializeObject(websocketEndpoint,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            Assert.Equal(expected, json);
        }

        public class Foo
        {
            public string Bar { get; set; }
        }

        
    }
}
