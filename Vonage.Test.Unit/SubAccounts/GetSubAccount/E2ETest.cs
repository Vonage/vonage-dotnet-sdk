using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.Test.Unit.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetSubAccount
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
        public async Task GetSubAccount()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/subaccounts/456iFuDL099")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result =
                await this.helper.VonageClient.SubAccountsClient.GetSubAccountAsync(
                    GetSubAccountRequest.Parse("456iFuDL099"));
            result.Should().BeSuccess();
        }
    }
}