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
    private const string BaseUri = "https://api.nexmo.com";
    private const string ApacUri = "https://api-ap.vonage.com";
    private const string EuUri = "https://api-eu.vonage.com";
    private const string UsUri = "https://api-us.vonage.com";
    private const string Endpoint = "/v1/calls";
    private readonly VonageClient client;
    private readonly Fixture fixture;

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(VoiceTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public VoiceTests()
    {
        this.fixture = new Fixture();
        this.client = this.BuildVonageClient(this.BuildCredentialsForBearerAuthentication());
    }

    public static TheoryData<VoiceTestsSetup> GetSetups =>
    [
        new VoiceTestsSetup(null, string.Concat(BaseUri, Endpoint)),
        new VoiceTestsSetup(VonageUrls.Region.APAC, string.Concat(ApacUri, Endpoint)),
        new VoiceTestsSetup(VonageUrls.Region.EU, string.Concat(EuUri, Endpoint)),
        new VoiceTestsSetup(VonageUrls.Region.US, string.Concat(UsUri, Endpoint)),
    ];

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

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task CreateCall(VoiceTestsSetup setup)
    {
        this.Setup(setup.BaseUri, this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.SetupClient(setup.Region).CreateCallAsync(VoiceTestData.CreateCallCommand());
        response.ShouldMatchExpectedCallResponse();
    }

    private IVoiceClient SetupClient(VonageUrls.Region? region) =>
        region.HasValue ? this.client.VoiceClient.WithRegion(region.Value) : this.client.VoiceClient;

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task CreateCallWithCredentials(VoiceTestsSetup setup)
    {
        this.Setup(setup.BaseUri, this.helper.GetResponseJson(nameof(this.CreateCall)),
            this.helper.GetRequestJson(nameof(this.CreateCall)));
        var response = await this.SetupClient(setup.Region).CreateCallAsync(VoiceTestData.CreateCallCommand(),
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

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task GetRecordings(VoiceTestsSetup setup)
    {
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(VoiceTestData.GetValidRecordingUri(), expectedResponse);
        var response = await this.SetupClient(setup.Region).GetRecordingAsync(VoiceTestData.GetValidRecordingUri());
        response.ShouldMatchExpectedRecording(expectedResponse);
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task GetRecordingsWithCredentials(VoiceTestsSetup setup)
    {
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        this.Setup(VoiceTestData.GetValidRecordingUri(), expectedResponse);
        var response = await this.SetupClient(setup.Region).GetRecordingAsync(VoiceTestData.GetValidRecordingUri(),
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

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task GetSpecificCall(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{setup.BaseUri}/{uuid}", this.helper.GetResponseJson());
        var callRecord = await this.SetupClient(setup.Region).GetCallAsync(uuid);
        callRecord.ShouldMatchExpectedCallRecord();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task GetSpecificCallWithCredentials(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{setup.BaseUri}/{uuid}", this.helper.GetResponseJson(nameof(this.GetSpecificCall)));
        var callRecord =
            await this.SetupClient(setup.Region).GetCallAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        callRecord.ShouldMatchExpectedCallRecord();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task ListCalls(VoiceTestsSetup setup)
    {
        this.Setup($"{setup.BaseUri}", this.helper.GetResponseJson());
        var callList = await this.SetupClient(setup.Region).GetCallsAsync(VoiceTestData.CreateBasicCallSearchFilter());
        callList.ShouldMatchExpectedCallsList();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task ListCallsWithCredentials(VoiceTestsSetup setup)
    {
        this.Setup(
            $"{setup.BaseUri}?status=started&date_start={WebUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&date_end={WebUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&page_size=10&record_index=0&order=asc&conversation_uuid=CON-f972836a-550f-45fa-956c-12a2ab5b7d22&",
            this.helper.GetResponseJson(nameof(this.ListCalls)));
        var callList = await this.SetupClient(setup.Region).GetCallsAsync(VoiceTestData.CreateComplexCallSearchFilter(),
            this.BuildCredentialsForBearerAuthentication());
        callList.ShouldMatchExpectedCallsList();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartDtmf(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/dtmf", this.helper.GetResponseJson(),
            this.helper.GetRequestJson());
        var response = await this.SetupClient(setup.Region).StartDtmfAsync(uuid, VoiceTestData.CreateDtmfCommand());
        response.ShouldMatchExpectedDtmfResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartDtmfWithCredentials(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/dtmf", this.helper.GetResponseJson(nameof(this.StartDtmf)),
            this.helper.GetRequestJson(nameof(this.StartDtmf)));
        var response = await this.SetupClient(setup.Region).StartDtmfAsync(uuid, VoiceTestData.CreateDtmfCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedDtmfResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartStream(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/stream", this.helper.GetResponseJson(),
            this.helper.GetRequestJson());
        var response = await this.SetupClient(setup.Region).StartStreamAsync(uuid, VoiceTestData.CreateStreamCommand());
        response.ShouldMatchExpectedStreamResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartStreamWithCredentials(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StartStream)),
            this.helper.GetRequestJson(nameof(this.StartStream)));
        var response = await this.SetupClient(setup.Region).StartStreamAsync(uuid, VoiceTestData.CreateStreamCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedStreamResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartTalk(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/talk", this.helper.GetResponseJson(),
            this.helper.GetRequestJson());
        var response = await this.SetupClient(setup.Region).StartTalkAsync(uuid, VoiceTestData.CreateTalkCommand());
        response.ShouldMatchExpectedTalkResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartTalkWithCredentials(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/talk", this.helper.GetResponseJson(nameof(this.StartTalk)),
            this.helper.GetRequestJson(nameof(this.StartTalk)));
        var response = await this.SetupClient(setup.Region).StartTalkAsync(uuid, VoiceTestData.CreateTalkCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedTalkResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StopStream(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/stream", this.helper.GetResponseJson());
        var response = await this.SetupClient(setup.Region).StopStreamAsync(uuid);
        response.ShouldMatchExpectedStopStreamResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StopStreamWithCredentials(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StopStream)));
        var response =
            await this.SetupClient(setup.Region).StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedStopStreamResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StopTalk(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/stream", this.helper.GetResponseJson());
        var response = await this.SetupClient(setup.Region).StopStreamAsync(uuid);
        response.ShouldMatchExpectedStopTalkResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StopTalkWithCredentials(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.Setup($"{setup.BaseUri}/{uuid}/stream", this.helper.GetResponseJson(nameof(this.StopTalk)));
        var response =
            await this.SetupClient(setup.Region).StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
        response.ShouldMatchExpectedStopTalkResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task SubscribeRealTimeDtmf(VoiceTestsSetup setup)
    {
        this.Setup($"{setup.BaseUri}/ID-123/input/dtmf", Maybe<string>.None, this.helper.GetRequestJson());
        await this.SetupClient(setup.Region).SubscribeRealTimeDtmf("ID-123", VoiceTestData.GetRealTimeDtmfUri());
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task UnsubscribeRealTimeDtmf(VoiceTestsSetup setup)
    {
        this.Setup($"{setup.BaseUri}/ID-123/input/dtmf", Maybe<string>.None, string.Empty);
        await this.SetupClient(setup.Region).UnsubscribeRealTimeDtmf("ID-123");
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task UpdateCall(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{setup.BaseUri}/{uuid}", Maybe<string>.None, this.helper.GetRequestJson());
        var response = await this.SetupClient(setup.Region)
            .UpdateCallAsync(uuid, VoiceTestData.CreateCallEditCommand());
        response.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task UpdateCallWithCredentials(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.Setup($"{setup.BaseUri}/{uuid}", Maybe<string>.None,
            this.helper.GetRequestJson(nameof(this.UpdateCall)));
        var response = await this.SetupClient(setup.Region).UpdateCallAsync(uuid, VoiceTestData.CreateCallEditCommand(),
            this.BuildCredentialsForBearerAuthentication());
        response.ShouldBeTrue();
    }

    private VonageClient BuildClientWithBasicAuthentication() =>
        new VonageClient(this.BuildCredentialsForBasicAuthentication());
}

public record VoiceTestsSetup(VonageUrls.Region? Region, string BaseUri);