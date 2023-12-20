using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.NumberInsightsV2.FraudCheck
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private const string ApiUrl = "Vonage.Url.Api";

        private readonly SerializationTestHelper serialization =
            new(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public async Task PerformFraudCheck_WithFraudScore_UsingBasicCredentials() =>
            await this.PerformFraudCheckWithFraudScore(TestingContext.WithBasicCredentials(ApiUrl));

        [Fact]
        public async Task PerformFraudCheck_WithFraudScore_UsingBearerCredentials() =>
            await this.PerformFraudCheckWithFraudScore(TestingContext.WithBearerCredentials(ApiUrl));

        [Fact]
        public async Task PerformFraudCheck_WithFraudScoreAndSimSwap_UsingBasicCredentials() =>
            await this.PerformFraudCheckWithFraudScoreAndSimSwap(TestingContext.WithBasicCredentials(ApiUrl));

        [Fact]
        public async Task PerformFraudCheck_WithFraudScoreAndSimSwap_UsingBearerCredentials() =>
            await this.PerformFraudCheckWithFraudScoreAndSimSwap(TestingContext.WithBearerCredentials(ApiUrl));

        [Fact]
        public async Task PerformFraudCheck_WithSimSwap_UsingBasicCredentials() =>
            await this.PerformFraudCheckWithSimSwap(TestingContext.WithBasicCredentials(ApiUrl));

        [Fact]
        public async Task PerformFraudCheck_WithSimSwap_UsingBearerCredentials() =>
            await this.PerformFraudCheckWithSimSwap(TestingContext.WithBearerCredentials(ApiUrl));

        private async Task PerformFraudCheck(TestingContext helper, string body, Result<FraudCheckRequest> request)
        {
            helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/ni")
                    .WithHeader("Authorization", helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(body)
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await helper.VonageClient.NumberInsightV2Client
                .PerformFraudCheckAsync(request)
                .Should()
                .BeSuccessAsync(SerializationTest.GetExpectedFraudCheckResponse());
        }

        private Task PerformFraudCheckWithFraudScore(TestingContext helper) =>
            this.PerformFraudCheck(helper, this.serialization.GetRequestJson(nameof(SerializationTest
                .ShouldSerializeWithFraudScore)), SerializationTest.BuildRequestWithFraudScore());

        private Task PerformFraudCheckWithFraudScoreAndSimSwap(TestingContext helper) =>
            this.PerformFraudCheck(helper, this.serialization.GetRequestJson(nameof(SerializationTest
                .ShouldSerializeWithFraudScoreAndSimSwap)), SerializationTest.BuildRequestWithFraudScoreAndSimSwap());

        private Task PerformFraudCheckWithSimSwap(TestingContext helper) =>
            this.PerformFraudCheck(helper, this.serialization.GetRequestJson(nameof(SerializationTest
                .ShouldSerializeWithSimSwap)), SerializationTest.BuildRequestWithSimSwap());
    }
}