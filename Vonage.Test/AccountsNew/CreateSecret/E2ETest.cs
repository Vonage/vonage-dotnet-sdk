using System.Net;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.AccountsNew.CreateSecret;

[Trait("Category", "E2E")]
[Trait("Product", "AccountsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task CreateSecret()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/accounts/abcd1234/secrets")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.localSerialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SecretInfoSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.AccountsNewClient
            .CreateSecretAsync(SerializationTest.BuildRequest())
            .Should()
            .BeSuccessAsync(SecretInfoSerializationTest.GetExpectedSecretInfo());
    }
}
