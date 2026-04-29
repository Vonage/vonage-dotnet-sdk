using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.AccountsNew.TopUpBalance;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.AccountsNew.TopUpBalance;

[Trait("Category", "E2E")]
[Trait("Product", "AccountsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task TopUpBalance()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/top-up")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.localSerialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.AccountsNewClient
            .TopUpBalanceAsync(TopUpBalanceRequest.Build()
                .WithTransactionReference("8ef2447e69604f642ae59363aa5f781b")
                .Create())
            .Should()
            .BeSuccessAsync(response =>
            {
                response.ErrorCode.Should().Be("200");
                response.ErrorCodeLabel.Should().Be("success");
            });
    }
}
