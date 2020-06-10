using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nexmo.Api.Test.Unit.Legacy
{
    public class CallTest : TestBase
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateCall(bool passCreds) 
        {
            var expectedUri = "https://api.nexmo.com/v1/calls/";
            var expectedResponse = @"{
              ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
              ""status"": ""started"",
              ""direction"": ""outbound"",
              ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22""
            }";
            var expectedRequesetContent = @"{""to"":[{""type"":""phone"",""number"":""14155550100"",""dtmfAnswer"":""p*123#""}],""from"":{""type"":""phone"",""number"":""14155550100"",""dtmfAnswer"":""p*123#""},""ncco"":[{""text"":""Hello World"",""action"":""talk""}],""answer_url"":[""https://example.com/answer""],""answer_method"":""GET"",""event_url"":[""https://example.com/event""],""event_method"":""POST"",""machine_detection"":""continue"",""length_timer"":1,""ringing_timer"":1}";
            Setup(expectedUri, expectedResponse, expectedRequesetContent);
            var request = new Voice.Call.CallCommand
            {
                to = new[]
                {
                    new Voice.Call.Endpoint
                    {
                        Number="14155550100",
                        DtmfAnswer="p*123#",
                        Type="phone"
                    }
                },
                from = new Voice.Call.Endpoint
                {
                    Type = "phone",
                    Number = "14155550100",
                    DtmfAnswer = "p*123#"
                },
                Ncco = new Voice.Nccos.Ncco(new Voice.Nccos.TalkAction { Text = "Hello World" }),
                answer_url = new[] { "https://example.com/answer" },
                answer_method = "GET",
                event_url=new[] { "https://example.com/event" },
                event_method="POST",
                machine_detection = "continue",
                length_timer = 1,
                ringing_timer = 1
            };
            var creds = Request.Credentials.FromAppIdAndPrivateKey(AppId, PrivateKey);
            var client = new Client(creds);
            Voice.Call.CallResponse response;
            if (passCreds)
            {
                response = client.Call.Do(request, creds);
            }
            else
            {
                response = client.Call.Do(request);
            }
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.conversation_uuid);
            Assert.Equal("outbound", response.direction);
            Assert.Equal("started", response.status);
        }
    }
}
