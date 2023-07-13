using System.Net;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.TransferAmount;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.TransferAmount.Credit
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        private readonly SerializationTestHelper serializationRequest;

        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
            this.serializationRequest = new SerializationTestHelper(typeof(TransferAmount.SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());
        }

        [Fact]
        public async Task TransferAmount()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/credit-transfers")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .WithBody(this.serializationRequest.GetRequestJson(nameof(SubAccounts.TransferAmount
                        .SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.SubAccountsClient.TransferCreditAsync(TransferAmountRequest
                .Build()
                .WithFrom("7c9738e6")
                .WithTo("ad6dc56f")
                .WithAmount((decimal) 123.45)
                .WithReference("This gets added to the audit log")
                .Create());
            result.Should().BeSuccess();
        }
    }
}