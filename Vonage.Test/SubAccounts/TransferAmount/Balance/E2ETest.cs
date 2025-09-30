#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.SubAccounts.TransferAmount;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.TransferAmount.Balance;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    private readonly SerializationTestHelper serializationRequest = new SerializationTestHelper(
        typeof(TransferAmount.SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task TransferAmount()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/accounts/790fc5e5/balance-transfers")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.serializationRequest.GetRequestJson(nameof(SubAccounts.TransferAmount.SerializationTest
                    .ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.SubAccountsClient.TransferBalanceAsync(TransferAmountRequest
                .Build()
                .WithFrom("7c9738e6")
                .WithTo("ad6dc56f")
                .WithAmount((decimal) 123.45)
                .WithReference("This gets added to the audit log")
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.GetExpectedTransfer());
    }
}