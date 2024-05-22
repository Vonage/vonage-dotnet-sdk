using System.Net;
using System.Threading.Tasks;
using Vonage.SimSwap.Check;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.SimSwap.Check;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }
    
    [Fact]
    public async Task CheckAsync()
    {
        this.SetupAuthorization();
        this.SetupToken();
        this.SetupSimSwap(nameof(SerializationTest.ShouldSerialize));
        await this.Helper.VonageClient.SimSwapClient
            .CheckAsync(CheckRequest.Build().WithPhoneNumber("346661113334").Create())
            .Should()
            .BeSuccessAsync(true);
    }
    
    [Fact]
    public async Task CheckAsyncWithPeriod()
    {
        this.SetupAuthorization();
        this.SetupToken();
        this.SetupSimSwap(nameof(SerializationTest.ShouldSerializeWithPeriod));
        await this.Helper.VonageClient.SimSwapClient
            .CheckAsync(CheckRequest.Build().WithPhoneNumber("346661113334").WithPeriod(15).Create())
            .Should()
            .BeSuccessAsync(true);
    }
    
    private void SetupSimSwap(string expectedOutput) =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/camara/sim-swap/v040/check")
                .WithHeader("Authorization", "Bearer ABCDEFG")
                .WithBody(this.Serialization.GetRequestJson(expectedOutput))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeCheck))));
    
    private void SetupToken() =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/token")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody("auth_req_id=123456789&grant_type=urn:openid:params:grant-type:ciba")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAccessToken))));
    
    private void SetupAuthorization() =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/bc-authorize")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(
                    "login_hint=tel:%2B346661113334&scope=openid+dpv%3AFraudPreventionAndDetection%23check-sim-swap")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAuthorize))));
}