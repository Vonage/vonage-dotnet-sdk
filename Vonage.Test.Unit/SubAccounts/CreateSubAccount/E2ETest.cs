using System.Net;
using System.Threading.Tasks;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.Test.Unit.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.CreateSubAccount
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task CreateSubAccount()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/subaccounts")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.SubAccountsClient.CreateSubAccountAsync(CreateSubAccountRequest
                    .Build()
                    .WithName("My SubAccount")
                    .WithSecret("123456789AbcDef")
                    .DisableSharedAccountBalance()
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedAccount());
        }

        [Fact]
        public async Task CreateSubAccountWithDefaultValues()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/subaccounts")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithDefaultValues)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.SubAccountsClient.CreateSubAccountAsync(CreateSubAccountRequest
                    .Build()
                    .WithName("My SubAccount")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedAccount());
        }
    }
}