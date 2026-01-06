#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.ShortCodes;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.ShortCodes;

[Trait("Category", "Legacy")]
public class ShortCodeTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ShortCodeTests).Namespace,
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

    private IShortCodesClient BuildShortCodesClient() => this.context.VonageClient.ShortCodesClient;

    [Fact]
    public async Task ManageOptIn()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sc/us/alert/opt-in/manage/json")
                .WithParam("msisdn", "15559301529")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildShortCodesClient()
            .ManageOptInAsync(ShortCodeTestData.CreateOptInManageRequest());
        response.ShouldMatchExpectedOptInResponse();
    }

    [Fact]
    public async Task QueryOptIns()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sc/us/alert/opt-in/query/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildShortCodesClient()
            .QueryOptInsAsync(ShortCodeTestData.CreateBasicOptInQueryRequest());
        response.ShouldMatchExpectedOptInQueryResponse();
    }

    [Fact]
    public async Task SendAlert()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sc/us/alert/json")
                .WithParam("to", "16365553226")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildShortCodesClient().SendAlertAsync(ShortCodeTestData.CreateBasicAlertRequest());
        response.ShouldMatchExpectedAlertResponse();
    }

    [Fact]
    public async Task SendTwoFactorAuth()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sc/us/2fa/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildShortCodesClient()
            .SendTwoFactorAuthAsync(ShortCodeTestData.CreateTwoFactorAuthRequest());
        response.ShouldMatchExpectedTwoFactorAuthResponse();
    }
}