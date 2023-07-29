using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.UpdateSubAccount;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.UpdateSubAccount
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task EnableSharedBalance()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerializeWithOnlyEnabledSharedBalance));
            await this.Helper.VonageClient.SubAccountsClient.UpdateSubAccountAsync(UpdateSubAccountRequest.Build()
                    .WithSubAccountKey("RandomKey")
                    .EnableSharedAccountBalance()
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedAccount());
        }

        [Fact]
        public async Task EnableSubAccount()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerializeWithOnlyEnabledAccount));
            await this.Helper.VonageClient.SubAccountsClient.UpdateSubAccountAsync(UpdateSubAccountRequest.Build()
                    .WithSubAccountKey("RandomKey")
                    .EnableAccount()
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedAccount());
        }

        [Fact]
        public async Task UpdateName()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerializeWithOnlyName));
            await this.Helper.VonageClient.SubAccountsClient.UpdateSubAccountAsync(UpdateSubAccountRequest
                    .Build()
                    .WithSubAccountKey("RandomKey")
                    .WithName("Subaccount department B")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedAccount());
        }

        [Fact]
        public async Task UpdateSubAccount()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerialize));
            await this.Helper.VonageClient.SubAccountsClient.UpdateSubAccountAsync(UpdateSubAccountRequest
                    .Build()
                    .WithSubAccountKey("RandomKey")
                    .WithName("Subaccount department B")
                    .SuspendAccount()
                    .DisableSharedAccountBalance()
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedAccount());
        }

        private void SetUpServer(string requestBody) =>
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/subaccounts/RandomKey")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .WithBody(this.Serialization.GetRequestJson(requestBody))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
    }
}