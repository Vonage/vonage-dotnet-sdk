using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.AccountsNew.GetSecrets;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.AccountsNew.GetSecrets;

[Trait("Category", "E2E")]
[Trait("Product", "AccountsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task GetSecrets()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/accounts/abcd1234/secrets")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.AccountsNewClient
            .GetSecretsAsync(GetSecretsRequest.Build()
                .WithApiKey("abcd1234")
                .Create())
            .Should()
            .BeSuccessAsync(response =>
            {
                response.Embedded.Secrets.Should().HaveCount(1);
                response.Embedded.Secrets[0].Id.Should().Be("ad6dc56f-07b5-46e1-a527-85530e625800");
            });
    }
}
