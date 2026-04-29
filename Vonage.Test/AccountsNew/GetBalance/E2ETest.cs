using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.AccountsNew.GetBalance;

[Trait("Category", "E2E")]
[Trait("Product", "AccountsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task GetBalance()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/get-balance")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.AccountsNewClient.GetBalanceAsync()
            .Should()
            .BeSuccessAsync(response =>
            {
                response.Value.Should().Be(10.28m);
                response.AutoReload.Should().BeFalse();
            });
    }
}
