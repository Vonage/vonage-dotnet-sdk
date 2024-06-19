using System.Net;
using System.Threading.Tasks;
using Vonage.NumberVerification.Verify;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.NumberVerification.Verify;

[Trait("Category", "E2E")]
public class E2ETest : SimSwap.E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task CheckAsync()
    {
        this.SetupAuthorization();
        this.SetupToken();
        this.SetupCheck(nameof(SerializationTest.ShouldSerialize));
        await this.Helper.VonageClient.NumberVerificationClient
            .VerifyAsync(VerifyRequest.Parse("346661113334"))
            .Should()
            .BeSuccessAsync(true);
    }

    private void SetupCheck(string expectedOutput) =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/camara/number-verification/v031/verify")
                .WithHeader("Authorization", "Bearer ABCDEFG")
                .WithBody(this.Serialization.GetRequestJson(expectedOutput))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeVerify))));

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
                .WithPath("/oauth2/auth")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(
                    "login_hint=tel:%2B346661113334&scope=openid+dpv%3AFraudPreventionAndDetection%23number-verification-verify-read")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAuthorize))));
}