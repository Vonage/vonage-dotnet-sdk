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
        public async Task PerformFraudCheck_WithFraudScoreAndSimSwap()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/ni")
                    .WithHeader("Authorization", "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithFraudScoreAndSimSwap)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.NumberInsightV2Client
                .PerformFraudCheckAsync(FraudCheckRequest.Build()
                    .WithPhone("447009000000")
                    .WithFraudScore()
                    .WithSimSwap()
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedFraudCheckResponse());
        }
    }
}