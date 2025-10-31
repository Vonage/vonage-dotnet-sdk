#region
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
using Xunit;
#endregion

namespace Vonage.Test.Voice;

[Trait("Category", "Legacy")]
public class NccoTests : TestBase
{
    private static readonly Regex TokenReplacementRegEx = new Regex(@"\$(\w+)\$", RegexOptions.Compiled);

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(NccoTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ConversationAction_StartOnEnter_ShouldBeTrue_GivenDefault() =>
        new ConversationAction().StartOnEnter.Should().BeTrue();

    [Fact]
    public void TestAppEndpoint() =>
        VerifyEndpoint(this.helper.GetRequestJson(), new AppEndpoint
        {
            User = "steve",
        });

    [Fact]
    public void TestConnect() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new ConnectAction
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
                    Shaken = "testShaken",
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
        });

    [Fact]
    public void TestConnectWithAdvancedMachineDetection() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new ConnectAction
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
        });

    [Fact]
    public void TestConversation() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new ConversationAction
        {
            Name = "vonage-conference-standard",
            MusicOnHoldUrl = new[] {"https://example.com/music.mp3"},
            StartOnEnter = false,
            EndOnExit = false,
            Record = false,
            CanSpeak = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
            CanHear = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
        });

    [Fact]
    public void TestConversationAllTrue() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new ConversationAction
        {
            Name = "vonage-conference-standard",
            MusicOnHoldUrl = new[] {"https://example.com/music.mp3"},
            StartOnEnter = true,
            EndOnExit = true,
            Record = true,
            CanSpeak = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
            CanHear = new[] {"6a4d6af0-55a6-4667-be90-8614e4c8e83c"},
        });

    [Fact]
    public void TestNotify() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new NotifyAction
        {
            EventMethod = "POST",
            Payload = new TestClass
            {
                Bar = "foo",
            },
            EventUrl = new[] {"https://example.com/webhooks/events"},
        });

    [Theory]
    [InlineData(RecordAction.AudioFormat.Mp3)]
    [InlineData(RecordAction.AudioFormat.Wav)]
    [InlineData(RecordAction.AudioFormat.Ogg)]
    public void TestRecord(RecordAction.AudioFormat audioFormat) =>
        VerifyNccoAction(this.GetRequestJsonWithValueReplacement(new Dictionary<string, string>
                {{"format", JsonConvert.SerializeObject(audioFormat, VonageSerialization.SerializerSettings)}}),
            new RecordAction
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
            });

    [Fact]
    public void TestRecordMinimalist() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new RecordAction());

    [Fact]
    public void TestSipEndpoint() =>
        VerifyEndpoint(this.helper.GetRequestJson(), new SipEndpoint
        {
            Uri = "sip:rebekka@sip.example.com",
            Headers = new TestClass {Bar = "foo"},
        });

    [Fact]
    public void TestSipEndpointWithStandardHeaders() =>
        VerifyEndpoint(this.helper.GetRequestJson(), new SipEndpoint
        {
            Uri = "sip:rebekka@sip.example.com",
            Headers = new TestClass {Bar = "foo"},
            StandardHeaders = new SipEndpoint.StandardHeader("UserToUser"),
        });

    [Fact]
    public void TestSipEndpointWithUserAndDomain() =>
        VerifyEndpoint(this.helper.GetRequestJson(), new SipEndpoint
        {
            User = "john.doe",
            Domain = "vonage.com",
            Headers = new TestClass {Bar = "foo"},
        });

    [Fact]
    public void TestStream() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new StreamAction
        {
            StreamUrl = new[] {"https://acme.com/streams/music.mp3"},
            BargeIn = true,
            Loop = "2",
            Level = "0",
        });

    [Fact]
    public void TestTalk() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new TalkAction
        {
            Text = "Hello World",
            BargeIn = true,
            Loop = "2",
            Level = "0",
            Language = "en-US",
            Style = 0,
        });

    [Fact]
    public void TestTalkBareBones() =>
        VerifyNccoAction(this.helper.GetRequestJson(), new TalkAction
        {
            Text = "Hello World",
        });

    [Fact]
    public void TestVbcEndpoint() =>
        VerifyEndpoint(this.helper.GetRequestJson(), new VbcEndpoint
        {
            Extension = "4567",
        });

    [Fact]
    public void TestWebsocketEndpoint() =>
        VerifyEndpoint(this.helper.GetRequestJson(), new WebsocketEndpoint
        {
            Uri = "wss://example.com/ws",
            ContentType = "audio/l16;rate=16000",
            Headers = new TestClass {Bar = "foo"},
        });

    [Fact]
    public void TransferAction() => VerifyNccoAction(this.helper.GetRequestJson(),
        new TransferAction("CON-f972836a-550f-45fa-956c-12a2ab5b7d22"));

    [Fact]
    public void TransferActionWithCanHear() => VerifyNccoAction(this.helper.GetRequestJson(),
        new TransferAction("CON-f972836a-550f-45fa-956c-12a2ab5b7d22").CanHearParticipant("PAR-123")
            .CanHearParticipant("PAR-456"));

    [Fact]
    public void TransferActionWithCanSpeak() => VerifyNccoAction(this.helper.GetRequestJson(),
        new TransferAction("CON-f972836a-550f-45fa-956c-12a2ab5b7d22").CanSpeakToParticipant("PAR-123")
            .CanSpeakToParticipant("PAR-456"));

    [Fact]
    public void TransferActionWithEmptyCanHear() => VerifyNccoAction(this.helper.GetRequestJson(),
        new TransferAction("CON-f972836a-550f-45fa-956c-12a2ab5b7d22").CanHearNoParticipants());

    [Fact]
    public void TransferActionWithEmptyCanSpeak() => VerifyNccoAction(this.helper.GetRequestJson(),
        new TransferAction("CON-f972836a-550f-45fa-956c-12a2ab5b7d22").CanSpeakToNoParticipants());

    [Fact]
    public void TransferActionWithMute() => VerifyNccoAction(this.helper.GetRequestJson(),
        new TransferAction("CON-f972836a-550f-45fa-956c-12a2ab5b7d22").Mute());

    [Fact]
    public void WaitAction() => VerifyNccoAction(this.helper.GetRequestJson(), new WaitAction());

    [Fact]
    public void WaitActionWithTimeout() => VerifyNccoAction(this.helper.GetRequestJson(), new WaitAction {Timeout = 2});

    private string GetRequestJsonWithValueReplacement(Dictionary<string, string> parameters,
        [CallerMemberName] string name = null) =>
        TokenReplacementRegEx.Replace(this.helper.GetRequestJson(name), match => parameters[match.Groups[1].Value]);

    private static void VerifyEndpoint<T>(string expected, T endpoint) where T : Endpoint =>
        Assert.Equal(expected, JsonConvert.SerializeObject(endpoint, VonageSerialization.SerializerSettings));

    private static void VerifyNccoAction<T>(string expected, T action) where T : NccoAction =>
        Assert.Equal(expected, new Ncco(action).ToString());

    private class TestClass
    {
        public string Bar { get; set; }
    }
}