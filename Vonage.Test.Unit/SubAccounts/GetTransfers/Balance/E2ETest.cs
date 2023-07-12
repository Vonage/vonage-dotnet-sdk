using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.GetTransfers;
using Vonage.Test.Unit.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetTransfers.Balance
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private readonly E2EHelper helper;
        private readonly SerializationTestHelper serialization;

        public E2ETest()
        {
            this.helper = SubAccountsHelper.BuildTestHelper();
            this.serialization = SubAccountsHelper.BuildSerializationHelper(typeof(SerializationTest).Namespace);
        }

        [Fact]
        public async Task GetBalanceTransfers()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/balance-transfers")
                    .WithParam("start_date", "2018-03-02T17:34:49Z")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.SubAccountsClient.GetBalanceTransfersAsync(GetTransfersRequest
                .Build()
                .WithStartDate(DateTimeOffset.Parse("2018-03-02T17:34:49Z"))
                .Create());
            result.Should().BeSuccess();
        }
    }
}