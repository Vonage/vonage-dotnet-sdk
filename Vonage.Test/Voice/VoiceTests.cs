#region
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
using Xunit;
#endregion

namespace Vonage.Test.Voice;

[Trait("Category", "Legacy")]
public class VoiceTests : TestBase
{
    private const string BaseUri = "https://api.nexmo.com/v1/calls";
    private readonly VonageClient client;
    private readonly Fixture fixture;

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(VoiceTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public VoiceTests()
    {
        this.fixture = new Fixture();
        this.client = this.BuildVonageClient(this.BuildCredentialsForBearerAuthentication());
    }

    [Theory]
    [InlineData(45)]
    [InlineData(120)]
    public void AdvancedMachineDetectionProperties_ShouldReturnInstance_GivenBeepTimeoutIsValid(int value)
    {
        var properties = new AdvancedMachineDetectionProperties(
            AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
            AdvancedMachineDetectionProperties.MachineDetectionMode.Detect,
            value);
        properties.Behavior.Should()
            .Be(AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue);
        properties.Mode.Should().Be(AdvancedMachineDetectionProperties.MachineDetectionMode.Detect);
        properties.BeepTimeout.Should().Be(value);
    }

    [Theory]
    [InlineData(44)]
    [InlineData(121)]
    public void AdvancedMachineDetectionProperties_ShouldThrowException_GivenBeepTimeoutIsInvalid(int value)
    {
        Action act = () => _ = new AdvancedMachineDetectionProperties(
            AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
            AdvancedMachineDetectionProperties.MachineDetectionMode.Detect,
            value);
        act.Should().ThrowExactly<VonageException>()
            .WithMessage("Beep Timeout has a minimal value of 45, and a maximal value of 120.");
    }

    [Fact]
    public async Task CreateCall()
    {
        this.Setup(BaseUri, this.helper.GetResponseJson(),
            this.helper.GetRequestJson());
        var request = BuildCreateCallCommand();
        var response = await this.client.VoiceClient.CreateCallAsync(request);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCall_WithCredentials()
    {
        this.Setup(BaseUri, this.helper.GetResponseJson(nameof(this.CreateCall)),
            this.helper.GetRequestJson(nameof(this.CreateCall)));
        var response = await this.client.VoiceClient.CreateCallAsync(BuildCreateCallCommand(),
            this.BuildCredentialsForBearerAuthentication());
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCall_WithInvalidCredentials_ShouldThrowException()
    {
        Func<Task> act = async () =>
            await this.BuildClientWithBasicAuthentication().VoiceClient.CreateCallAsync(new CallCommand());
        await act.Should().ThrowExactlyAsync<VonageAuthenticationException>()
            .WithMessage("AppId or Private Key Path missing.");
    }

    [Fact]
    public async Task GetRecordings()
    {
        var expectedUri = "https://api.nexmo.com/v1/files/aaaaaaaa-bbbb-cccc-dddd-0123456789ab";
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(expectedUri, expectedResponse);
        var response = await this.client.VoiceClient.GetRecordingAsync(expectedUri);
        Assert.Equal(expectedResponse, response.ResultStream);
    }

    [Theory]
    [InlineData("https://example.com/v1/abc123")]
    [InlineData("http://api.sample.com/v1/abc123")]
    public async Task GetRecordings_ShouldThrowException_GivenDomainIsRejected(string invalidUri)
    {
        var act = () => this.client.VoiceClient.GetRecordingAsync(invalidUri);
        await act.Should().ThrowAsync<VonageException>().WithMessage("Invalid uri");
    }

    [Theory]
    [InlineData("not a url")]
    [InlineData("abc123")]
    public async Task GetRecordings_ShouldThrowException_GivenUriIsInvalid(string invalidUri)
    {
        var act = () => this.client.VoiceClient.GetRecordingAsync(invalidUri);
        await act.Should().ThrowAsync<VonageException>().WithMessage("Invalid uri");
    }

    [Fact]
    public async Task GetRecordings_WithCredentials()
    {
        var expectedUri = "https://api.nexmo.com/v1/files/aaaaaaaa-bbbb-cccc-dddd-0123456789ab";
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(expectedUri, expectedResponse);
        var response =
            await this.client.VoiceClient.GetRecordingAsync(expectedUri,
                this.BuildCredentialsForBearerAuthentication());
        Assert.Equal(expectedResponse, response.ResultStream);
    }

    [Fact]
    public async Task GetSpecificCall()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{BaseUri}/{uuid}", this.helper.GetResponseJson());
        var callRecord = await this.client.VoiceClient.GetCallAsync(uuid);
        AssertSpecificCall(callRecord);
    }

    [Fact]
    public async Task GetSpecificCall_WithCredentials()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{BaseUri}/{uuid}", this.helper.GetResponseJson(nameof(this.GetSpecificCall)));
        var callRecord =
            await this.client.VoiceClient.GetCallAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        AssertSpecificCall(callRecord);
    }

    [Fact]
    public async Task ListCalls()
    {
        var filter = new CallSearchFilter();
        this.Setup($"{this.ApiUrl}/v1/calls", this.helper.GetResponseJson());
        var callList = await this.client.VoiceClient.GetCallsAsync(filter);
        var callRecord = callList.Embedded.Calls[0];
        AssertListCalls(callList, callRecord);
    }

    [Fact]
    public async Task ListCalls_WithCredentials()
    {
        var filter = new CallSearchFilter
        {
            ConversationUuid = "CON-f972836a-550f-45fa-956c-12a2ab5b7d22",
            DateStart = DateTime.Parse("2016-11-14T07:45:14"),
            DateEnd = DateTime.Parse("2016-11-14T07:45:14"),
            PageSize = 10,
            RecordIndex = 0,
            Order = "asc",
            Status = "started",
        };
        this.Setup(
            $"{BaseUri}?status=started&date_start={WebUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&date_end={WebUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&page_size=10&record_index=0&order=asc&conversation_uuid=CON-f972836a-550f-45fa-956c-12a2ab5b7d22&",
            this.helper.GetResponseJson(nameof(this.ListCalls)));
        var callList =
            await this.client.VoiceClient.GetCallsAsync(filter, this.BuildCredentialsForBearerAuthentication());
        var callRecord = callList.Embedded.Calls[0];
        AssertListCalls(callList, callRecord);
    }

    [Fact]
    public async Task StartDtmf()
    {
        var uuid = this.fixture.Create<string>();
        var command = new DtmfCommand {Digits = "1234"};
        this.Setup($"{BaseUri}/{uuid}/dtmf", this.helper.GetResponseJson(),
            this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.StartDtmfAsync(uuid, command);
        AssertStartDtmf(response);
    }

    [Fact]
    public async Task StartDtmf_WithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        var command = new DtmfCommand {Digits = "1234"};
        this.Setup($"{BaseUri}/{uuid}/dtmf", this.helper.GetResponseJson(nameof(this.StartDtmf)),
            this.helper.GetRequestJson(nameof(this.StartDtmf)));
        var response =
            await this.client.VoiceClient.StartDtmfAsync(uuid, command,
                this.BuildCredentialsForBearerAuthentication());
        AssertStartDtmf(response);
    }

    [Fact]
    public async Task StartStream()
    {
        var uuid = this.fixture.Create<string>();
        var command = new StreamCommand
        {
            StreamUrl = new[] {"https://example.com/waiting.mp3"},
            Loop = 0,
            Level = "0.4",
        };
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.StartStreamAsync(uuid, command);
        AssertStartStream(response);
    }

    [Fact]
    public async Task StartStream_WithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        var command = new StreamCommand
        {
            StreamUrl = new[] {"https://example.com/waiting.mp3"},
            Loop = 0,
            Level = "0.4",
        };
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StartStream)),
            this.helper.GetRequestJson(nameof(this.StartStream)));
        var response =
            await this.client.VoiceClient.StartStreamAsync(uuid, command,
                this.BuildCredentialsForBearerAuthentication());
        AssertStartStream(response);
    }

    [Fact]
    public async Task StartTalk()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/talk", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.StartTalkAsync(uuid, new TalkCommand
        {
            Text = "Hello. How are you today?",
            Language = "en-US",
            Loop = 0,
            Level = "0.4",
            Premium = true,
            Style = 1,
        });
        AssertStartTalk(response);
    }

    [Fact]
    public async Task StartTalk_WithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/talk", this.helper.GetResponseJson(nameof(this.StartTalk)),
            this.helper.GetRequestJson(nameof(this.StartTalk)));
        var response = await this.client.VoiceClient.StartTalkAsync(uuid, new TalkCommand
        {
            Text = "Hello. How are you today?",
            Language = "en-US",
            Loop = 0,
            Level = "0.4",
            Premium = true,
            Style = 1,
        }, this.BuildCredentialsForBearerAuthentication());
        AssertStartTalk(response);
    }

    [Fact]
    public async Task StopStream()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson());
        var response = await this.client.VoiceClient.StopStreamAsync(uuid);
        AssertStopStream(response);
    }

    [Fact]
    public async Task StopStreamAsync_Credentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StopStream)));
        var response =
            await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        AssertStopStream(response);
    }

    [Fact]
    public async Task StopTalk()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson());
        var response = await this.client.VoiceClient.StopStreamAsync(uuid);
        AssertStopTalk(response);
    }

    [Fact]
    public async Task StopTalk_WithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StopTalk)));
        var response =
            await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        AssertStopTalk(response);
    }

    [Fact]
    public async Task SubscribeRealTimeDtmf()
    {
        this.Setup($"{BaseUri}/ID-123/input/dtmf", Maybe<string>.None, this.helper.GetRequestJson());
        await this.client.VoiceClient.SubscribeRealTimeDtmf("ID-123", new Uri("https://example.com/ivr"));
    }

    [Fact]
    public async Task UnsubscribeRealTimeDtmf()
    {
        this.Setup($"{BaseUri}/ID-123/input/dtmf", Maybe<string>.None, string.Empty);
        await this.client.VoiceClient.UnsubscribeRealTimeDtmf("ID-123");
    }

    [Fact]
    public async Task UpdateCall()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        var request = new CallEditCommand
        {
            Destination = new Destination
            {
                Type = "ncco",
                Ncco = new Ncco(new TalkAction {Text = "hello world"}),
                Url = new[] {"https://example.com/ncco.json"}
            },
            Action = CallEditCommand.ActionType.transfer,
        };
        this.Setup($"{BaseUri}/{uuid}", Maybe<string>.None, this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.UpdateCallAsync(uuid, request);
        Assert.True(response);
    }

    [Fact]
    public async Task UpdateCall_WithCredentials()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        var request = new CallEditCommand
        {
            Destination = new Destination
            {
                Type = "ncco",
                Ncco = new Ncco(new TalkAction {Text = "hello world"}),
                Url = new[] {"https://example.com/ncco.json"}
            },
            Action = CallEditCommand.ActionType.transfer,
        };
        this.Setup($"{BaseUri}/{uuid}", Maybe<string>.None, this.helper.GetRequestJson(nameof(this.UpdateCall)));
        var response =
            await this.client.VoiceClient.UpdateCallAsync(uuid, request,
                this.BuildCredentialsForBearerAuthentication());
        Assert.True(response);
    }

    private static void AssertListCalls(PageResponse<CallList> callList, CallRecord callRecord)
    {
        Assert.True(100 == callList.Count);
        Assert.True(10 == callList.PageSize);
        Assert.True(0 == callList.PageIndex);
        Assert.Equal("/calls?page_size=10&record_index=20&order=asc", callList.Links.Self.Href);
        Assert.Equal("/calls/63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Links.Self.Href);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", callRecord.ConversationUuid);
        Assert.Equal("447700900000", callRecord.To.Number);
        Assert.Equal("phone", callRecord.To.Type);
        Assert.Equal("phone", callRecord.From.Type);
        Assert.Equal("447700900001", callRecord.From.Number);
        Assert.Equal("started", callRecord.Status);
        Assert.Equal("outbound", callRecord.Direction);
        Assert.Equal("0.39", callRecord.Rate);
        Assert.Equal("23.40", callRecord.Price);
        Assert.Equal("60", callRecord.Duration);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), callRecord.StartTime);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), callRecord.EndTime);
    }

    private static void AssertSpecificCall(CallRecord callRecord)
    {
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", callRecord.ConversationUuid);
        Assert.Equal("447700900000", callRecord.To.Number);
        Assert.Equal("phone", callRecord.To.Type);
        Assert.Equal("phone", callRecord.From.Type);
        Assert.Equal("447700900001", callRecord.From.Number);
        Assert.Equal("started", callRecord.Status);
        Assert.Equal("outbound", callRecord.Direction);
        Assert.Equal("0.39", callRecord.Rate);
        Assert.Equal("23.40", callRecord.Price);
        Assert.Equal("60", callRecord.Duration);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), callRecord.StartTime);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), callRecord.EndTime);
    }

    private static void AssertStartDtmf(CallCommandResponse response)
    {
        Assert.Equal("DTMF sent", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    private static void AssertStartStream(CallCommandResponse response)
    {
        Assert.Equal("Stream started", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    private static void AssertStartTalk(CallCommandResponse response)
    {
        Assert.Equal("Talk started", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    private static void AssertStopStream(CallCommandResponse response)
    {
        Assert.Equal("Stream stopped", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    private static void AssertStopTalk(CallCommandResponse response)
    {
        Assert.Equal("Talk stopped", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    private VonageClient BuildClientWithBasicAuthentication() =>
        new VonageClient(this.BuildCredentialsForBasicAuthentication());

    private static CallCommand BuildCreateCallCommand() =>
        new CallCommand
        {
            To = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = "14155550100",
                    DtmfAnswer = "p*123#",
                },
            },
            From = new PhoneEndpoint
            {
                Number = "14155550100",
                DtmfAnswer = "p*123#",
            },
            Ncco = new Ncco(new TalkAction {Text = "Hello World"},
                new TalkAction {Text = "Hello World", Premium = true}, new TalkAction {Text = "בדיקה בדיקה בדיקה"},
                new MultiInputAction {Mode = MultiInputAction.InputMode.Asynchronous}),
            AnswerUrl = new[] {"https://example.com/answer"},
            AnswerMethod = "GET",
            EventUrl = new[] {"https://example.com/event"},
            EventMethod = "POST",
            MachineDetection = "continue",
            LengthTimer = 1,
            RingingTimer = 1,
            AdvancedMachineDetection = new AdvancedMachineDetectionProperties(
                AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                AdvancedMachineDetectionProperties.MachineDetectionMode.Detect, 45),
            RandomFromNumber = true
        };
}