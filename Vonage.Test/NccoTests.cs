﻿#region
using System.Collections.Generic;
using System.Net.Http;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Serialization;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
using Xunit;
#endregion

namespace Vonage.Test;

[Trait("Category", "Legacy")]
public class NccoTests : TestBase
{
    [Fact]
    public void ConversationAction_StartOnEnter_ShouldBeTrue_GivenDefault() =>
        new ConversationAction().StartOnEnter.Should().BeTrue();

    [Fact]
    public void TestAppEndpoint()
    {
        var expectedJson = this.GetRequestJson();
        var endpoint = new AppEndpoint
        {
            User = "steve",
        };
        Assert.Equal(expectedJson,
            JsonConvert.SerializeObject(endpoint, VonageSerialization.SerializerSettings));
    }

    [Fact]
    public void TestConnect()
    {
        var expectedJson = this.GetRequestJson();
        var connectAction = new ConnectAction
        {
            Endpoint = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = "447700900001",
                    DtmfAnswer = "2p02p",
                    OnAnswer = new PhoneEndpoint.Answer
                    {
                        Url = "https://example.com/answer",
                        RingbackTone = "http://example.com/ringbackTone.wav",
                    },
                },
            },
            From = "447700900000",
            EventType = "synchronous",
            Timeout = "60",
            Limit = "7200",
            MachineDetection = "continue",
            EventUrl = new[] {"https://exampe.com/webhooks/events"},
            RingbackTone = "http://example.com/ringbackTone.wav",
            EventMethod = "POST",
        };
        var ncco = new Ncco(connectAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestConnectWithAdvancedMachineDetection()
    {
        var connectAction = new ConnectAction
        {
            Endpoint = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = "447700900001",
                    DtmfAnswer = "2p02p",
                    OnAnswer = new PhoneEndpoint.Answer
                    {
                        Url = "https://example.com/answer",
                        RingbackTone = "http://example.com/ringbackTone.wav",
                    },
                },
            },
            From = "447700900000",
            EventType = "synchronous",
            Timeout = "60",
            Limit = "7200",
            MachineDetection = "continue",
            EventUrl = new[] {"https://exampe.com/webhooks/events"},
            RingbackTone = "http://example.com/ringbackTone.wav",
            EventMethod = "POST",
            AdvancedMachineDetection = new AdvancedMachineDetectionProperties(
                AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                AdvancedMachineDetectionProperties.MachineDetectionMode.Detect, 45),
        };
        Assert.Equal(this.GetRequestJson(), new Ncco(connectAction).ToString());
    }

    [Fact]
    public void TestConversation()
    {
        var expectedJson = this.GetRequestJson();
        var conversationAction = new ConversationAction
        {
            Name = "vonage-conference-standard",
            MusicOnHoldUrl = new[] {"https://example.com/music.mp3"},
            StartOnEnter = false,
            EndOnExit = false,
            Record = false,
            CanSpeak = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
            CanHear = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
        };
        var ncco = new Ncco(conversationAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestConversationAllTrue()
    {
        var expectedJson = this.GetRequestJson();
        var conversationAction = new ConversationAction
        {
            Name = "vonage-conference-standard",
            MusicOnHoldUrl = new[] {"https://example.com/music.mp3"},
            StartOnEnter = true,
            EndOnExit = true,
            Record = true,
            CanSpeak = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
            CanHear = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
        };
        var ncco = new Ncco(conversationAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestNotify()
    {
        var expectedJson = this.GetRequestJson();
        var notifyAction = new NotifyAction
        {
            EventMethod = "POST",
            Payload = new TestClass
            {
                Bar = "foo",
            },
            EventUrl = new[] {"https://example.com/webhooks/events"},
        };
        var ncco = new Ncco(notifyAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Theory]
    [InlineData(RecordAction.AudioFormat.Mp3)]
    [InlineData(RecordAction.AudioFormat.Wav)]
    [InlineData(RecordAction.AudioFormat.Ogg)]
    public void TestRecord(RecordAction.AudioFormat audioFormat)
    {
        var parameters = new Dictionary<string, string>();
        parameters.Add("format", JsonConvert.SerializeObject(audioFormat, VonageSerialization.SerializerSettings));
        var expectedJson = this.GetRequestJson(parameters);
        var recordAction = new RecordAction
        {
            Format = audioFormat,
            Split = "conversation",
            Channels = 2,
            EndOnSilence = "3",
            EndOnKey = "#",
            TimeOut = "60",
            BeepStart = true,
            EventUrl = new[] {"https://example.com/record", "https://test.com/record"},
            EventMethod = "POST",
            Transcription = new RecordAction.TranscriptionSettings
            {
                EventMethod = HttpMethod.Get,
                EventUrl = new[] {"https://example.com"},
                Language = "en-US",
                SentimentAnalysis = true,
            },
        };
        var ncco = new Ncco(recordAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestRecordMinimalist()
    {
        var expectedJson = this.GetRequestJson();
        var recordAction = new RecordAction();
        var ncco = new Ncco(recordAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestSipEndpoint()
    {
        var expectedJson = this.GetRequestJson();
        var endpoint = new SipEndpoint
        {
            Uri = "sip:rebekka@sip.example.com",
            Headers = new TestClass {Bar = "foo"},
        };
        Assert.Equal(expectedJson,
            JsonConvert.SerializeObject(endpoint, VonageSerialization.SerializerSettings));
    }

    [Fact]
    public void TestSipEndpointWithStandardHeaders()
    {
        var expectedJson = this.GetRequestJson();
        var endpoint = new SipEndpoint
        {
            Uri = "sip:rebekka@sip.example.com",
            Headers = new TestClass {Bar = "foo"},
            StandardHeaders = new SipEndpoint.StandardHeader("UserToUser"),
        };
        Assert.Equal(expectedJson,
            JsonConvert.SerializeObject(endpoint, VonageSerialization.SerializerSettings));
    }

    [Fact]
    public void TestSipEndpointWithUserAndDomain()
    {
        var expectedJson = this.GetRequestJson();
        var endpoint = new SipEndpoint
        {
            User = "john.doe",
            Domain = "vonage.com",
            Headers = new TestClass {Bar = "foo"},
        };
        Assert.Equal(expectedJson,
            JsonConvert.SerializeObject(endpoint, VonageSerialization.SerializerSettings));
    }

    [Fact]
    public void TestStream()
    {
        var expectedJson = this.GetRequestJson();
        var talkAction = new StreamAction
        {
            StreamUrl = new[] {"https://acme.com/streams/music.mp3"},
            BargeIn = true,
            Loop = "2",
            Level = "0",
        };
        var ncco = new Ncco(talkAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestTalk()
    {
        var expectedJson = this.GetRequestJson();
        var talkAction = new TalkAction
        {
            Text = "Hello World",
            BargeIn = true,
            Loop = "2",
            Level = "0",
            Language = "en-US",
            Style = 0,
        };
        var ncco = new Ncco(talkAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestTalkBareBones()
    {
        var expectedJson = this.GetRequestJson();
        var talkAction = new TalkAction
        {
            Text = "Hello World",
        };
        var ncco = new Ncco(talkAction);
        var actualJson = ncco.ToString();
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void TestVbcEndpoint()
    {
        var expectedJson = this.GetRequestJson();
        var endpoint = new VbcEndpoint
        {
            Extension = "4567",
        };
        Assert.Equal(expectedJson,
            JsonConvert.SerializeObject(endpoint, VonageSerialization.SerializerSettings));
    }

    [Fact]
    public void TestWebsocketEndpoint()
    {
        var expectedJson = this.GetRequestJson();
        var endpoint = new WebsocketEndpoint
        {
            Uri = "wss://example.com/ws",
            ContentType = "audio/l16;rate=16000",
            Headers = new TestClass {Bar = "foo"},
        };
        Assert.Equal(expectedJson,
            JsonConvert.SerializeObject(endpoint, VonageSerialization.SerializerSettings));
    }

    private class TestClass
    {
        public string Bar { get; set; }
    }
}