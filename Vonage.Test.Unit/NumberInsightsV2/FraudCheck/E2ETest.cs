using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.NumberInsightV2.FraudCheck;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.NumberInsightsV2.FraudCheck
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task CreateEmptyUser()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/ni")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldDeserialize200WithFraudScoreAndSimSwap)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.NumberInsightV2Client
                .PerformFraudCheckAsync(FraudCheckRequest.Build())
                .Should()
                .BeSuccessAsync();
        }
    }
}