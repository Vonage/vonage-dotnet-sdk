#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using Vonage.Verify;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Verify;

[Trait("Category", "Legacy")]
public class VerifyTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(VerifyTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IResponseBuilder RespondWithSuccess([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    private IVerifyClient BuildVerifyClient() => this.context.VonageClient.VerifyClient;

    [Fact]
    public async Task Psd2Verification()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/verify/psd2/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildVerifyClient()
            .VerifyRequestWithPSD2Async(VerifyTestData.CreateBasicPsd2Request());
        response.ShouldMatchExpectedVerifyResponse();
    }

    [Fact]
    public async Task RequestVerification()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/verify/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildVerifyClient().VerifyRequestAsync(VerifyTestData.CreateBasicVerifyRequest());
        response.ShouldMatchExpectedVerifyResponse();
    }

    [Fact]
    public async Task TestCheckVerification()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/verify/check/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildVerifyClient().VerifyCheckAsync(VerifyTestData.CreateBasicVerifyCheckRequest());
        response.ShouldMatchExpectedVerifyCheckResponse();
    }

    [Fact]
    public async Task TestControlVerify()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/verify/control/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildVerifyClient().VerifyControlAsync(VerifyTestData.CreateVerifyControlRequest());
        response.ShouldMatchExpectedVerifyControlResponse();
    }

    [Fact]
    public async Task TestControlVerifyInvalidCredentials()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/verify/control/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var ex = await Assert.ThrowsAsync<VonageVerifyResponseException>(() =>
            this.BuildVerifyClient().VerifyControlAsync(VerifyTestData.CreateVerifyControlRequest()));
        ex.ShouldThrowVerifyResponseException();
    }

    [Fact]
    public async Task TestVerifySearch()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/verify/search/json")
                .WithParam("request_id", "abcdef0123456789abcdef0123456789")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildVerifyClient().VerifySearchAsync(VerifyTestData.CreateVerifySearchRequest());
        response.ShouldMatchExpectedVerifySearchResponse();
    }
}