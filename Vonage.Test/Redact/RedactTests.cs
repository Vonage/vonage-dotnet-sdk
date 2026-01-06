#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.Common.Exceptions;
using Vonage.Redaction;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Redact;

[Trait("Category", "Legacy")]
public class RedactTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(RedactTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IResponseBuilder RespondWithSuccess([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson("Redact"));

    private IResponseBuilder RespondWithError(HttpStatusCode statusCode, [CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(statusCode)
            .WithBody(this.helper.GetResponseJson(testName));

    private IRedactClient BuildRedactClient() => this.context.VonageClient.RedactClient;

    [Fact]
    public async Task RedactMessagesInbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateMessagesInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactMessagesOutbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateMessagesOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactNumberInsightInbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateNumberInsightInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactNumberInsightOutbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateNumberInsightOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactReturns401()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithError(HttpStatusCode.Unauthorized));
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns403()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithError(HttpStatusCode.Forbidden));
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns404()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithError(HttpStatusCode.NotFound));
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns422()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithError(HttpStatusCode.UnprocessableEntity));
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns429()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithError(HttpStatusCode.TooManyRequests));
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactSmsInbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateSmsInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactSmsOutbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateSmsOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifyInbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifyInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifyOutbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifyOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifySdkInbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifySdkInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifySdkOutbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifySdkOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVoiceInbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVoiceInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVoiceOutbound()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/redact/transaction")
                .WithBody(this.helper.GetRequestJson())
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVoiceOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }
}