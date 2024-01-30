using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.SubAccounts.GetTransfers;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.SubAccounts.GetTransfers.Balance;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetBalanceTransfers()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/accounts/790fc5e5/balance-transfers")
                .WithParam("start_date", "2018-03-02T17:34:49Z")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.SubAccountsClient.GetBalanceTransfersAsync(GetTransfersRequest
                .Build()
                .WithStartDate(DateTimeOffset.Parse("2018-03-02T17:34:49Z"))
                .Create())
            .Should()
            .BeSuccessAsync(success =>
                success.Should().BeEquivalentTo(SerializationTest.GetExpectedTransfers()));
    }
}