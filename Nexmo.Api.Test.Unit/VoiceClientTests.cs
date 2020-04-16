using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Nexmo.Api.Voice;
namespace Nexmo.Api.Test.Unit
{
    public class VoiceClientTests : TestBase
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
            var expectedRequesetContent = @"{""to"":[{""number"":""14155550100"",""dtmfAnswer"":""p*123#"",""type"":""phone""}],""from"":{""number"":""14155550100"",""dtmfAnswer"":""p*123#"",""type"":""phone""},""ncco"":[{""text"":""Hello World"",""action"":""talk""}],""answer_url"":[""https://example.com/answer""],""answer_method"":""GET"",""event_url"":[""https://example.com/event""],""event_method"":""POST"",""machine_detection"":""continue"",""length_timer"":1,""ringing_timer"":1}";

            Setup(expectedUri, expectedResponse, expectedRequesetContent);

            var request = new Voice.CallCommand
            {
                To = new[] 
                { 
                    new Voice.Nccos.Endpoints.PhoneEndpoint
                    {
                        Number="14155550100",
                        DtmfAnswer="p*123#"
                    }
                },
                From = new Voice.Nccos.Endpoints.PhoneEndpoint
                {
                    Number = "14155550100",
                    DtmfAnswer = "p*123#"
                },
                Ncco = new Voice.Nccos.Ncco(new Voice.Nccos.TalkAction { Text="Hello World"}),
                AnswerUrl = new [] { "https://example.com/answer" },
                AnswerMethod="GET",
                EventUrl= new[] { "https://example.com/event" },
                EventMethod="POST",
                MachineDetection="continue",
                LengthTimer=1,
                RingingTimer=1,
            };
            var creds = Request.Credentials.FromAppIdAndPrivateKey(AppId, PrivateKey);
            var client = new NexmoClient(creds);
            Voice.CallResponse response;
            if (passCreds)
            {
                response = client.VoiceClient.CreateCall(request, creds);
            }
            else
            {
                response = client.VoiceClient.CreateCall(request);
            }
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }
    }
}
