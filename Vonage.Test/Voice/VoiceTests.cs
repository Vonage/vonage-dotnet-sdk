#region
using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Voice;
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

    [Fact]
    public void AdvancedMachineDetectionPropertiesWithInvalidBeepTimeout()
    {
        Action act = () => VoiceTestData.CreateValidAdvancedMachineDetectionProperties(44);
        act.Should().ThrowExactly<VonageException>()
            .WithMessage("Beep Timeout has a minimal value of 45, and a maximal value of 120.");
    }

    [Fact]
    public void AdvancedMachineDetectionPropertiesWithValidBeepTimeout()
    {
        var properties = VoiceTestData.CreateValidAdvancedMachineDetectionProperties();
        properties.ShouldHaveValidAdvancedMachineDetectionProperties(45);
    }

    [Fact]
    public async Task CreateCall()
    {
        this.Setup(BaseUri, this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.CreateCallAsync(VoiceTestData.CreateCallCommand());
        response.ShouldMatchExpectedCallResponse();
    }

    [Fact]
    public async Task CreateCallWithCredentials()
    {
        this.Setup(BaseUri, this.helper.GetResponseJson(nameof(this.CreateCall)),
            this.helper.GetRequestJson(nameof(this.CreateCall)));
        var response = await this.client.VoiceClient.CreateCallAsync(VoiceTestData.CreateCallCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedCallResponse();
    }

    [Fact]
    public async Task CreateCallWithInvalidCredentials()
    {
        Func<Task> act = async () =>
            await this.BuildClientWithBasicAuthentication().VoiceClient.CreateCallAsync(new CallCommand());
        await act.Should().ThrowExactlyAsync<VonageAuthenticationException>()
            .WithMessage("AppId or Private Key Path missing.");
    }

    [Fact]
    public async Task GetRecordings()
    {
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(VoiceTestData.GetValidRecordingUri(), expectedResponse);
        var response = await this.client.VoiceClient.GetRecordingAsync(VoiceTestData.GetValidRecordingUri());
        response.ShouldMatchExpectedRecording(expectedResponse);
    }

    [Fact]
    public async Task GetRecordingsWithCredentials()
    {
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(VoiceTestData.GetValidRecordingUri(), expectedResponse);
        var response = await this.client.VoiceClient.GetRecordingAsync(VoiceTestData.GetValidRecordingUri(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedRecording(expectedResponse);
    }

    [Fact]
    public async Task GetRecordingsWithInvalidDomain()
    {
        var act = () => this.client.VoiceClient.GetRecordingAsync(VoiceTestData.GetInvalidDomainUri());
        await act.Should().ThrowAsync<VonageException>().WithMessage("Invalid uri");
    }

    [Fact]
    public async Task GetRecordingsWithInvalidUri()
    {
        var act = () => this.client.VoiceClient.GetRecordingAsync(VoiceTestData.GetInvalidUri());
        await act.Should().ThrowAsync<VonageException>().WithMessage("Invalid uri");
    }

    [Fact]
    public async Task GetSpecificCall()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{BaseUri}/{uuid}", this.helper.GetResponseJson());
        var callRecord = await this.client.VoiceClient.GetCallAsync(uuid);
        callRecord.ShouldMatchExpectedCallRecord();
    }

    [Fact]
    public async Task GetSpecificCallWithCredentials()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{BaseUri}/{uuid}", this.helper.GetResponseJson(nameof(this.GetSpecificCall)));
        var callRecord =
            await this.client.VoiceClient.GetCallAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        callRecord.ShouldMatchExpectedCallRecord();
    }

    [Fact]
    public async Task ListCalls()
    {
        this.Setup($"{this.ApiUrl}/v1/calls", this.helper.GetResponseJson());
        var callList = await this.client.VoiceClient.GetCallsAsync(VoiceTestData.CreateBasicCallSearchFilter());
        callList.ShouldMatchExpectedCallsList();
    }

    [Fact]
    public async Task ListCallsWithCredentials()
    {
        this.Setup(
            $"{BaseUri}?status=started&date_start={WebUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&date_end={WebUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&page_size=10&record_index=0&order=asc&conversation_uuid=CON-f972836a-550f-45fa-956c-12a2ab5b7d22&",
            this.helper.GetResponseJson(nameof(this.ListCalls)));
        var callList = await this.client.VoiceClient.GetCallsAsync(VoiceTestData.CreateComplexCallSearchFilter(),
            this.BuildCredentialsForBearerAuthentication());
        callList.ShouldMatchExpectedCallsList();
    }

    [Fact]
    public async Task StartDtmf()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/dtmf", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.StartDtmfAsync(uuid, VoiceTestData.CreateDtmfCommand());
        response.ShouldMatchExpectedDtmfResponse();
    }

    [Fact]
    public async Task StartDtmfWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/dtmf", this.helper.GetResponseJson(nameof(this.StartDtmf)),
            this.helper.GetRequestJson(nameof(this.StartDtmf)));
        var response = await this.client.VoiceClient.StartDtmfAsync(uuid, VoiceTestData.CreateDtmfCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedDtmfResponse();
    }

    [Fact]
    public async Task StartStream()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.StartStreamAsync(uuid, VoiceTestData.CreateStreamCommand());
        response.ShouldMatchExpectedStreamResponse();
    }

    [Fact]
    public async Task StartStreamWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StartStream)),
            this.helper.GetRequestJson(nameof(this.StartStream)));
        var response = await this.client.VoiceClient.StartStreamAsync(uuid, VoiceTestData.CreateStreamCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedStreamResponse();
    }

    [Fact]
    public async Task StartTalk()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/talk", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.StartTalkAsync(uuid, VoiceTestData.CreateTalkCommand());
        response.ShouldMatchExpectedTalkResponse();
    }

    [Fact]
    public async Task StartTalkWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/talk", this.helper.GetResponseJson(nameof(this.StartTalk)),
            this.helper.GetRequestJson(nameof(this.StartTalk)));
        var response = await this.client.VoiceClient.StartTalkAsync(uuid, VoiceTestData.CreateTalkCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedTalkResponse();
    }

    [Fact]
    public async Task StopStream()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson());
        var response = await this.client.VoiceClient.StopStreamAsync(uuid);
        response.ShouldMatchExpectedStopStreamResponse();
    }

    [Fact]
    public async Task StopStreamWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StopStream)));
        var response =
            await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedStopStreamResponse();
    }

    [Fact]
    public async Task StopTalk()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson());
        var response = await this.client.VoiceClient.StopStreamAsync(uuid);
        response.ShouldMatchExpectedStopTalkResponse();
    }

    [Fact]
    public async Task StopTalkWithCredentials()
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StopTalk)));
        var response =
            await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedStopTalkResponse();
    }

    [Fact]
    public async Task SubscribeRealTimeDtmf()
    {
        this.Setup($"{BaseUri}/ID-123/input/dtmf", Maybe<string>.None, this.helper.GetRequestJson());
        await this.client.VoiceClient.SubscribeRealTimeDtmf("ID-123", VoiceTestData.GetRealTimeDtmfUri());
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
        this.Setup($"{BaseUri}/{uuid}", Maybe<string>.None, this.helper.GetRequestJson());
        var response = await this.client.VoiceClient.UpdateCallAsync(uuid, VoiceTestData.CreateCallEditCommand());
        response.ShouldBeTrue();
    }

    [Fact]
    public async Task UpdateCallWithCredentials()
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{BaseUri}/{uuid}", Maybe<string>.None, this.helper.GetRequestJson(nameof(this.UpdateCall)));
        var response = await this.client.VoiceClient.UpdateCallAsync(uuid, VoiceTestData.CreateCallEditCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldBeTrue();
    }

    private VonageClient BuildClientWithBasicAuthentication() =>
        new VonageClient(this.BuildCredentialsForBasicAuthentication());
}