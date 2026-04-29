using System.Net;
using System.Threading.Tasks;
using Vonage.AccountsNew.GetSecret;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.AccountsNew.GetSecret;

[Trait("Category", "E2E")]
[Trait("Product", "AccountsNew")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task GetSecret()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/accounts/abcd1234/secrets/ad6dc56f-07b5-46e1-a527-85530e625800")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SecretInfoSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.AccountsNewClient
            .GetSecretAsync(GetSecretRequest.Build()
                .WithApiKey("abcd1234")
                .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
                .Create())
            .Should()
            .BeSuccessAsync(SecretInfoSerializationTest.GetExpectedSecretInfo());
    }
}
