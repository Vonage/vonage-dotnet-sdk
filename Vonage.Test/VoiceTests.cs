using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
using Xunit;

namespace Vonage.Test;

[Trait("Category", "Legacy")]
public class VoiceTests : TestBase
{
    private const string BaseUri = "https://api.nexmo.com/v1/calls";
    private readonly VonageClient client;
    private readonly Fixture fixture;

    public VoiceTests()
    {
        this.fixture = new Fixture();
        this.client = this.BuildVonageClient(BuildCredentialsForBearerAuthentication());
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
    public async Task CreateCallAsync()
    {
        this.Setup(BaseUri, this.GetResponseJson(),
            this.GetRequestJson());
        var request = BuildCreateCallCommand();
        var response = await this.client.VoiceClient.CreateCallAsync(request);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCallAsyncWithCredentials()
    {
        this.Setup(BaseUri, this.GetResponseJson(nameof(this.CreateCallAsync)),
            this.GetRequestJson(nameof(this.CreateCallAsync)));
        var response = await this.client.VoiceClient.CreateCallAsync(BuildCreateCallCommand(),
            this.BuildCredentialsForBearerAuthentication());
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCallAsyncWithRandomFromNumber()
    {
        this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
        var request = BuildCreateCallCommand();
        request.From = null;
        request.AdvancedMachineDetection = null;
        request.RandomFromNumber = true;
        this.BuildCredentialsForBearerAuthentication();
        var response = await this.client.VoiceClient.CreateCallAsync(request);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCallAsyncWithWrongCredsThrowsAuthException()
    {
        Func<Task> act = async () =>
            await this.BuildClientWithBasicAuthentication().VoiceClient.CreateCallAsync(new CallCommand());
        await act.Should().ThrowExactlyAsync<VonageAuthenticationException>()
            .WithMessage("AppId or Private Key Path missing.");
    }

    [Fact]
    public async Task CreateCallWithPremiumTalkActionAsync()
    {
        this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
        var response = await this.client.VoiceClient.CreateCallAsync(new CallCommand
        {
            To = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = "14155550100",
                },
            },
            From = new PhoneEndpoint
            {
                Number = "14155550100",
            },
            Ncco = new Ncco(new TalkAction {Text = "Hello World", Premium = true}),
        });
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCallWithStringParametersAsync()
    {
        this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
        var response = await this.client.VoiceClient.CreateCallAsync(new CallCommand
        {
            To = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = "14155550100",
                },
            },
            From = new PhoneEndpoint
            {
                Number = "14155550100",
            },
            Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
        });
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCallWithUnicodeCharacters()
    {
        this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
        var request = BuildCreateCallCommand();
        request.Ncco = new Ncco(new TalkAction {Text = "בדיקה בדיקה בדיקה"});
        var response = await this.client.VoiceClient.CreateCallAsync(request);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
        Assert.Equal("outbound", response.Direction);
        Assert.Equal("started", response.Status);
    }

    [Fact]
    public async Task CreateCallWithWrongCredsThrowsAuthException()
    {
        var act = () => this.BuildClientWithBasicAuthentication()
            .VoiceClient.CreateCallAsync(new CallCommand());
        await act.Should().ThrowExactlyAsync<VonageAuthenticationException>()
            .WithMessage("AppId or Private Key Path missing.");
    }

    [Fact]
    public async Task GetRecordingsAsync()
    {
        var expectedUri = this.fixture.Create<Uri>().ToString();
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(expectedUri, expectedResponse);
        var response = await this.client.VoiceClient.GetRecordingAsync(expectedUri);
        Assert.Equal(expectedResponse, response.ResultStream);
    }

    [Fact]
    public async Task GetRecordingsAsyncWithCredentials()
    {
        var expectedUri = this.fixture.Create<Uri>().ToString();
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(expectedUri, expectedResponse);
        var response =
            await this.client.VoiceClient.GetRecordingAsync(expectedUri,
                this.BuildCredentialsForBearerAuthentication());
        Assert.Equal(expectedResponse, response.ResultStream);
    }

    [Fact]
    public async Task GetSpecificCallAsync()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{BaseUri}/{uuid}", this.GetResponseJson());
        var callRecord = await this.client.VoiceClient.GetCallAsync(uuid);
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

    [Fact]
    public async Task GetSpecificCallAsyncWithCredentials()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{BaseUri}/{uuid}", this.GetResponseJson(nameof(this.GetSpecificCallAsync)));
        var callRecord =
            await this.client.VoiceClient.GetCallAsync(uuid, this.BuildCredentialsForBearerAuthentication());
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

    [Fact]
    public async Task ListCallsAsync()
    {
        var filter = new CallSearchFilter();
        this.Setup($"{this.ApiUrl}/v1/calls", this.GetResponseJson());
        var callList = await this.client.VoiceClient.GetCallsAsync(filter);
        var callRecord = callList.Embedded.Calls[0];
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

    [Fact]
    public async Task ListCallsAsyncWithCredentials()
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
            this.GetResponseJson(nameof(this.ListCallsAsync)));
        var callList =
            await this.client.VoiceClient.GetCallsAsync(filter, this.BuildCredentialsForBearerAuthentication());
        var callRecord = callList.Embedded.Calls[0];
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

    [Fact]
    public async Task StartDtmfAsync()
    {
        var uuid = this.fixture.Create<string>();
        var command = new DtmfCommand {Digits = "1234"};
        this.Setup($"{BaseUri}/{uuid}/dtmf", this.GetResponseJson(),
            this.GetRequestJson());
        var response = await this.client.VoiceClient.StartDtmfAsync(uuid, command);
        Assert.Equal("DTMF sent", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StartDtmfAsyncWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        var command = new DtmfCommand {Digits = "1234"};
        this.Setup($"{BaseUri}/{uuid}/dtmf", this.GetResponseJson(nameof(this.StartDtmfAsync)),
            this.GetRequestJson(nameof(this.StartDtmfAsync)));
        var response =
            await this.client.VoiceClient.StartDtmfAsync(uuid, command,
                this.BuildCredentialsForBearerAuthentication());
        Assert.Equal("DTMF sent", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StartStreamAsync()
    {
        var uuid = this.fixture.Create<string>();
        var command = new StreamCommand
        {
            StreamUrl = new[] {"https://example.com/waiting.mp3"},
        };
        this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(), this.GetRequestJson());
        var response = await this.client.VoiceClient.StartStreamAsync(uuid, command);
        Assert.Equal("Stream started", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StartStreamAsyncWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        var command = new StreamCommand
        {
            StreamUrl = new[] {"https://example.com/waiting.mp3"},
            Loop = 0,
            Level = "0.4",
        };
        this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StartStreamAsync)),
            this.GetRequestJson());
        var response =
            await this.client.VoiceClient.StartStreamAsync(uuid, command,
                this.BuildCredentialsForBearerAuthentication());
        Assert.Equal("Stream started", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StartTalkAsync()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/talk", this.GetResponseJson(), this.GetRequestJson());
        var response = await this.client.VoiceClient.StartTalkAsync(uuid, new TalkCommand
        {
            Text = "Hello. How are you today?",
        });
        Assert.Equal("Talk started", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StartTalkAsyncWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/talk", this.GetResponseJson(nameof(this.StartTalkAsync)),
            this.GetRequestJson(nameof(this.StartTalkAsync)));
        var response = await this.client.VoiceClient.StartTalkAsync(uuid, new TalkCommand
        {
            Text = "Hello. How are you today?",
        }, this.BuildCredentialsForBearerAuthentication());
        Assert.Equal("Talk started", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StopStreamAsync()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson());
        var response = await this.client.VoiceClient.StopStreamAsync(uuid);
        Assert.Equal("Stream stopped", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StopStreamAsyncWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopStreamAsync)));
        var response =
            await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        Assert.Equal("Stream stopped", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StopTalkAsync()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson());
        var response = await this.client.VoiceClient.StopStreamAsync(uuid);
        Assert.Equal("Talk stopped", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task StopTalkAsyncWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopTalkAsync)));
        var response =
            await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        Assert.Equal("Talk stopped", response.Message);
        Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
    }

    [Fact]
    public async Task UpdateCallAsync()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        var request = new CallEditCommand {Action = CallEditCommand.ActionType.earmuff};
        this.Setup($"{BaseUri}/{uuid}", string.Empty, this.GetRequestJson());
        var response = await this.client.VoiceClient.UpdateCallAsync(uuid, request);
        Assert.True(response);
    }

    [Fact]
    public async Task UpdateCallWithCredentials()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        var request = new CallEditCommand
        {
            Destination = new Destination {Type = "ncco", Url = new[] {"https://example.com/ncco.json"}},
            Action = CallEditCommand.ActionType.transfer,
        };
        this.Setup($"{BaseUri}/{uuid}", string.Empty, this.GetRequestJson());
        var response =
            await this.client.VoiceClient.UpdateCallAsync(uuid, request,
                this.BuildCredentialsForBearerAuthentication());
        Assert.True(response);
    }

    [Fact]
    public async Task UpdateCallWithInlineNcco()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        var request = new CallEditCommand
        {
            Destination = new Destination {Type = "ncco", Ncco = new Ncco(new TalkAction {Text = "hello world"})},
            Action = CallEditCommand.ActionType.transfer,
        };
        this.Setup($"{BaseUri}/{uuid}", string.Empty, this.GetRequestJson());
        Assert.True(await this.client.VoiceClient.UpdateCallAsync(uuid, request));
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
            Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
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
        };
}