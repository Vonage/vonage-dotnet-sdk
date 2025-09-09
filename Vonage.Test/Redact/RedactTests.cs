#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Exceptions;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Redact;

[Trait("Category", "Legacy")]
public class RedactTests : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(RedactTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task RedactMessagesInbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateMessagesInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactMessagesOutbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateMessagesOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactNumberInsightInbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateNumberInsightInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactNumberInsightOutbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateNumberInsightOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactReturns401()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson(),
            expectedCode: HttpStatusCode.Unauthorized);
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns403()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson(),
            expectedCode: HttpStatusCode.Forbidden);
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns404()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson(),
            expectedCode: HttpStatusCode.NotFound);
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns422()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson(),
            expectedCode: HttpStatusCode.UnprocessableEntity);
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactReturns429()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson(),
            expectedCode: HttpStatusCode.TooManyRequests);
        var exception = await Assert.ThrowsAsync<VonageHttpRequestException>(() =>
            this.BuildRedactClient().RedactAsync(RedactTestData.CreateErrorTestRequest()));
        exception.ShouldBeHttpRequestException(this.helper.GetResponseJson());
    }

    [Fact]
    public async Task RedactSmsInbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateSmsInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactSmsOutbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateSmsOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifyInbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifyInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifyOutbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifyOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifySdkInbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifySdkInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVerifySdkOutbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVerifySdkOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVoiceInbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVoiceInboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    [Fact]
    public async Task RedactVoiceOutbound()
    {
        this.Setup($"{this.ApiUrl}/v1/redact/transaction", this.helper.GetResponseJson("Redact"),
            this.helper.GetRequestJson());
        var response = await this.BuildRedactClient().RedactAsync(RedactTestData.CreateVoiceOutboundRequest());
        response.ShouldBeSuccessfulRedaction();
    }

    private IRedactClient BuildRedactClient() =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).RedactClient;
}