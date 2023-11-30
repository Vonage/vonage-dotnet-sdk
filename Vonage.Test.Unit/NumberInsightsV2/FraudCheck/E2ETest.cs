using System.Net;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Test.Unit.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.NumberInsightsV2.FraudCheck
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private readonly E2EHelper helperWithBasicCredentials = E2EHelper.WithBasicCredentials("Vonage.Url.Api");

        private readonly E2EHelper helperWithBearerCredentials = E2EHelper.WithBearerCredentials("Vonage.Url.Api");

        private readonly SerializationTestHelper serialization =
            new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public async Task PerformFraudCheck_WithFraudScoreAndSimSwap_UsingBasicCredentials() =>
            await this.PerformFraudCheckWithFraudScoreAndSimSwap(this.helperWithBasicCredentials);

        [Fact]
        public async Task PerformFraudCheck_WithFraudScoreAndSimSwap_UsingBearerCredentials() =>
            await this.PerformFraudCheckWithFraudScoreAndSimSwap(this.helperWithBearerCredentials);

        private async Task PerformFraudCheckWithFraudScoreAndSimSwap(E2EHelper helper)
        {
            helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/ni")
                    .WithHeader("Authorization", helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithFraudScoreAndSimSwap)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await helper.VonageClient.NumberInsightV2Client
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