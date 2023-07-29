using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.TransferNumber;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.TransferNumber
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task TransferNumber()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/transfer-number")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.SubAccountsClient.TransferNumberAsync(TransferNumberRequest
                    .Build()
                    .WithFrom("7c9738e6")
                    .WithTo("ad6dc56f")
                    .WithNumber("23507703696")
                    .WithCountry("GB")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedResponse());
        }
    }
}