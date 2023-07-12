using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.UpdateSubAccount;
using Vonage.Test.Unit.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.UpdateSubAccount
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
        public async Task UpdateSubAccount()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/subaccounts/RandomKey")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.SubAccountsClient.UpdateSubAccountAsync(UpdateSubAccountRequest
                .Build()
                .WithSubAccountKey("RandomKey")
                .WithName("Subaccount department B")
                .SuspendAccount()
                .DisableSharedAccountBalance()
                .Create());
            result.Should().BeSuccess();
        }
    }
}