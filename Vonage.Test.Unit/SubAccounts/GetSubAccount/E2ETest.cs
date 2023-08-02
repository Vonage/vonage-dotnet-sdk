using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.GetSubAccount;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetSubAccount
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetSubAccount()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/subaccounts/456iFuDL099")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.SubAccountsClient.GetSubAccountAsync(
                    GetSubAccountRequest.Parse("456iFuDL099"))
                .Should()
                .BeSuccessAsync();
        }
    }
}