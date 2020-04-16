using Newtonsoft.Json;
using Nexmo.Api.Voice.Nccos;
using Nexmo.Api.Voice.Nccos.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nexmo.Api.Test.Unit
{
    public class NccoTests : TestBase
    {
        [Fact]
        public void TestRecord()
        {
            var expectedJson = @"[{""format"":""mp3"",""split"":""conversation"",""channels"":2,""endOnSilence"":""3"",""endOnKey"":""#"",""timeOut"":""60"",""beepStart"":""true"",""eventUrl"":[""https://example.com/record""],""eventMethod"":""POST"",""action"":""record""}]";
            var recordAction = new RecordAction
            {
                Format = RecordAction.AudioFormat.mp3,
                Split = "conversation",
                Channels = 2,
                EndOnSilence = "3",
                EndOnKey = "#",
                TimeOut = "60",
                BeepStart = "true",
                EventUrl = new[] { "https://example.com/record" },
                EventMethod = "POST"
            };

            var ncco = new Ncco(recordAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }

        [Fact]
        public void TestConversation()
        {
            var expectedJson = @"[{""name"":""nexmo-conference-standard"",""musicOnHoldUrl"":[""https://example.com/music.mp3""],""startOnEnter"":""true"",""endOnExit"":""false"",""record"":""true"",""canSpeak"":[""6a4d6af0-55a6-4667-be90-8614e4c8e83c""],""canHear"":[""6a4d6af0-55a6-4667-be90-8614e4c8e83c""],""action"":""conversation""}]";
            var conversationAction = new ConversationAction
            {
                Name = "nexmo-conference-standard",
                MusicOnHoldUrl = new[] { "https://example.com/music.mp3" },
                StartOnEnter = "true",
                EndOnExit = "false",
                Record = "true",
                CanSpeak = new[] { "6a4d6af0-55a6-4667-be90-8614e4c8e83c" },
                CanHear = new[] { "6a4d6af0-55a6-4667-be90-8614e4c8e83c" }
            };
            var ncco = new Ncco(conversationAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }

        [Fact]
        public void TestConnect()
        {
            var expectedJson = @"[{""endpoint"":[{""number"":""447700900001"",""dtmfAnswer"":""2p02p"",""onAnswer"":{""url"":""https://example.com/answer"",""ringbackTone"":""http://example.com/ringbackTone.wav""},""type"":""phone""}],""from"":""447700900000"",""eventType"":""synchronous"",""timeout"":""60"",""limit"":""7200"",""machineDetection"":""continue"",""eventUrl"":[""https://exampe.com/webhooks/events""],""eventMethod"":""POST"",""ringbackTone"":""http://example.com/ringbackTone.wav"",""action"":""connect""}]";
            var connectAction = new ConnectAction
            {
                Endpoint = new[]
                {
                    new PhoneEndpoint
                    {
                        Number="447700900001",
                        DtmfAnswer="2p02p",
                        OnAnswer=new PhoneEndpoint.Answer
                        {
                            Url="https://example.com/answer",
                            RingbackTone="http://example.com/ringbackTone.wav"
                        }
                    }
                },
                From = "447700900000",
                EventType = "synchronous",
                Timeout = "60",
                Limit = "7200",
                MachineDetection = "continue",
                EventUrl = new[] { "https://exampe.com/webhooks/events" },
                RingbackTone = "http://example.com/ringbackTone.wav",
                EventMethod = "POST"
            };
            var ncco = new Ncco(connectAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }

        [Fact]
        public void TestTalk()
        {
            var expectedJson = @"[{""text"":""Hello World"",""bargeIn"":""true"",""loop"":""2"",""level"":""0"",""voiceName"":""kimberly"",""action"":""talk""}]";
            var talkAction = new TalkAction
            {
                Text = "Hello World",
                BargeIn = "true",
                Loop = "2",
                Level = "0",
                VoiceName = "kimberly"
            };
            var ncco = new Ncco(talkAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }
        
        [Fact]
        public void TestTalkBareBones()
        {
            var expectedJson = @"[{""text"":""Hello World"",""action"":""talk""}]";
            var talkAction = new TalkAction
            {
                Text = "Hello World"
            };
            var ncco = new Ncco(talkAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }

        [Fact]
        public void TestStream()
        {
            var expectedJson = @"[{""streamUrl"":[""https://acme.com/streams/music.mp3""],""level"":""0"",""bargeIn"":""true"",""loop"":""2"",""action"":""stream""}]";
            var talkAction = new StreamAction
            {
                StreamUrl = new[] { "https://acme.com/streams/music.mp3"},
                BargeIn = "true",
                Loop = "2",
                Level = "0",                
            };
            var ncco = new Ncco(talkAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }

        [Fact]
        public void TestInput()
        {
            var expectedJson = @"[{""timeOut"":""3"",""maxDigits"":""4"",""submitOnHash"":""true"",""eventUrl"":[""https://example.com/ivr""],""eventMethod"":""POST"",""action"":""input""}]";
            var inputAction = new InputAction
            {
                TimeOut = "3",
                MaxDigits = 4,
                SubmitOnHash = "true",
                EventUrl = new[] { "https://example.com/ivr" },
                EventMethod = "POST"
            };
            var ncco = new Ncco(inputAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }

        [Fact]
        public void TestNotify()
        {
            var expectedJson = @"[{""payload"":{""Bar"":""foo""},""eventUrl"":[""https://example.com/webhooks/events""],""eventMethod"":""POST"",""action"":""notify""}]";
            var notifyAction = new NotifyAction
            {
                EventMethod = "POST",
                Payload = new Foo
                {
                    Bar = "foo"
                },
                EventUrl = new[] { "https://example.com/webhooks/events" }
            };
            var ncco = new Ncco(notifyAction);
            Assert.Equal(expectedJson, ncco.ToString());
        }

        [Fact]
        public void TestWebsocketEndpoint()
        {
            var expectedJson = @"{""uri"":""wss://example.com/ws"",""content-type"":""audio/l16;rate=16000"",""headers"":{""Bar"":""foo""},""type"":""websocket""}";
            var endpoint = new WebsocketEndpoint
            {
                Uri = "wss://example.com/ws",
                ContentType = "audio/l16;rate=16000",
                Headers = new Foo { Bar = "foo" }
            };
            Assert.Equal(expectedJson, JsonConvert.SerializeObject(endpoint));
        }

        [Fact]
        public void TestAppEndpoint()
        {
            var expectedJson = @"{""user"":""steve"",""type"":""app""}";
            var endpoint = new AppEndpoint
            {
                User= "steve"
            };
            Assert.Equal(expectedJson, JsonConvert.SerializeObject(endpoint));
        }

        [Fact]
        public void TestSipEndpoint()
        {
            var expectedJson = @"{""uri"":""sip:rebekka@sip.example.com"",""headers"":{""Bar"":""foo""},""type"":""sip""}";
            var endpoint = new SipEndpoint
            {
                Uri = "sip:rebekka@sip.example.com",
                Headers = new Foo { Bar="foo"}
            };
            Assert.Equal(expectedJson, JsonConvert.SerializeObject(endpoint));
        }

        [Fact]
        public void TestVbcEndpoint()
        {
            var expectedJson = @"{""extension"":""4567"",""type"":""vbc""}";
            var endpoint = new VbcEndpoint
            {
                Extension = "4567"
            };
            Assert.Equal(expectedJson, JsonConvert.SerializeObject(endpoint));

        }
        public class Foo
        {
            public string Bar { get; set; }
        }

        
    }
}
