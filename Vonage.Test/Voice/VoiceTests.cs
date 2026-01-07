#region
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Vonage.Common.Exceptions;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.TestHelpers;
using Vonage.Voice;
using WireMock.ResponseBuilders;
using Xunit;
using TimeProvider = Vonage.Common.TimeProvider;
#endregion

namespace Vonage.Test.Voice;

[Trait("Category", "Legacy")]
public class VoiceTests
{
    private const string Endpoint = "/v1/calls";
    private readonly VoiceTestingContext context = VoiceTestingContext.WithBearerCredentials();
    private readonly Fixture fixture = new Fixture();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(VoiceTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public static TheoryData<VoiceTestsSetup> GetSetups =>
    [
        new VoiceTestsSetup(null, Endpoint),
        new VoiceTestsSetup(VonageUrls.Region.APAC, string.Concat("/APAC", Endpoint)),
        new VoiceTestsSetup(VonageUrls.Region.EU, string.Concat("/EMEA", Endpoint)),
        new VoiceTestsSetup(VonageUrls.Region.US, string.Concat("/AMER", Endpoint)),
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
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(setup.BaseUri)
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPost())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var response = await this.SetupClient(setup.Region).CreateCallAsync(VoiceTestData.CreateCallCommand());
        response.ShouldMatchExpectedCallResponse();
    }

    private IVoiceClient SetupClient(VonageUrls.Region? region) =>
        region.HasValue
            ? this.context.VonageClient.VoiceClient.WithRegion(region.Value)
            : this.context.VonageClient.VoiceClient;

    [Fact]
    public async Task GetRecordings()
    {
        var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        var response = await this.BuildClientWithMessageHandlerForRecordings(expectedResponse)
            .GetRecordingAsync(VoiceTestData.GetValidRecordingUri());
        response.ShouldMatchExpectedRecording(expectedResponse);
    }

    [Fact]
    public async Task GetRecordingsWithInvalidDomain()
    {
        var act = () => this.context.VonageClient.VoiceClient.GetRecordingAsync(VoiceTestData.GetInvalidDomainUri());
        await act.Should().ThrowAsync<VonageException>().WithMessage("Invalid uri");
    }

    [Fact]
    public async Task GetRecordingsWithInvalidUri()
    {
        var act = () => this.context.VonageClient.VoiceClient.GetRecordingAsync(VoiceTestData.GetInvalidUri());
        await act.Should().ThrowAsync<VonageException>().WithMessage("Invalid uri");
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task GetSpecificCall(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/{uuid}")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var callRecord = await this.SetupClient(setup.Region).GetCallAsync(uuid);
        callRecord.ShouldMatchExpectedCallRecord();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task ListCalls(VoiceTestsSetup setup)
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(setup.BaseUri)
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var callList = await this.SetupClient(setup.Region).GetCallsAsync(VoiceTestData.CreateBasicCallSearchFilter());
        callList.ShouldMatchExpectedCallsList();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartDtmf(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/{uuid}/dtmf")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPut())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var response = await this.SetupClient(setup.Region).StartDtmfAsync(uuid, VoiceTestData.CreateDtmfCommand());
        response.ShouldMatchExpectedDtmfResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartStream(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/{uuid}/stream")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPut())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var response = await this.SetupClient(setup.Region)
            .StartStreamAsync(uuid, VoiceTestData.CreateStreamCommand());
        response.ShouldMatchExpectedStreamResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StartTalk(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/{uuid}/talk")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPut())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var response = await this.SetupClient(setup.Region).StartTalkAsync(uuid, VoiceTestData.CreateTalkCommand());
        response.ShouldMatchExpectedTalkResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StopStream(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/{uuid}/stream")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var response = await this.SetupClient(setup.Region).StopStreamAsync(uuid);
        response.ShouldMatchExpectedStopStreamResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task StopTalk(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<string>();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/{uuid}/talk")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create()
                .WithBody(this.helper.GetResponseJson())
                .WithStatusCode(HttpStatusCode.OK));
        var response = await this.SetupClient(setup.Region).StopTalkAsync(uuid);
        response.ShouldMatchExpectedStopTalkResponse();
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task SubscribeRealTimeDtmf(VoiceTestsSetup setup)
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/ID-123/input/dtmf")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPut())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.NoContent));
        await this.SetupClient(setup.Region).SubscribeRealTimeDtmf("ID-123", VoiceTestData.GetRealTimeDtmfUri());
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task UnsubscribeRealTimeDtmf(VoiceTestsSetup setup)
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/ID-123/input/dtmf")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.NoContent));
        await this.SetupClient(setup.Region).UnsubscribeRealTimeDtmf("ID-123");
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task UpdateCall(VoiceTestsSetup setup)
    {
        var uuid = this.fixture.Create<Guid>().ToString();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"{setup.BaseUri}/{uuid}")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPut())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.NoContent));
        var response = await this.SetupClient(setup.Region)
            .UpdateCallAsync(uuid, VoiceTestData.CreateCallEditCommand());
        response.ShouldBeTrue();
    }

    private IVoiceClient BuildClientWithMessageHandlerForRecordings(byte[] expectedData)
    {
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri.AbsoluteUri == VoiceTestData.GetValidRecordingUri()),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new MemoryStream(expectedData)),
            })
            .Verifiable();
        var config = new Configuration {ClientHandler = mockHandler.Object};
        return new VonageClient(
            Credentials.FromAppIdAndPrivateKey("afed99d2-ae38-487c-bb5a-fe2518febd44", TokenHelper.GetKey()), config,
            new TimeProvider()).VoiceClient;
    }
}

public record VoiceTestsSetup(VonageUrls.Region? Region, string BaseUri);