using System;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.NumberInsightV2.FraudCheck;
using Xunit;

namespace Vonage.Test.Unit.NumberInsightsV2.FraudCheck
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() => this.helper.Serializer
            .DeserializeObject<FraudCheckResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(GetExpectedFraudCheckResponse());

        [Fact]
        public void ShouldSerializeWithFraudScore() => BuildRequestWithFraudScore()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithFraudScoreAndSimSwap() => BuildRequestWithFraudScoreAndSimSwap()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithSimSwap() => BuildRequestWithSimSwap()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

        internal static Result<FraudCheckRequest> BuildRequestWithFraudScoreAndSimSwap() =>
            FraudCheckRequest.Build()
                .WithPhone("447009000000")
                .WithFraudScore()
                .WithSimSwap()
                .Create();

        internal static Result<FraudCheckRequest> BuildRequestWithSimSwap() =>
            FraudCheckRequest.Build()
                .WithPhone("447009000000")
                .WithSimSwap()
                .Create();

        internal static FraudCheckResponse GetExpectedFraudCheckResponse() => new FraudCheckResponse(
            new Guid("6cb4c489-0fc8-4c40-8c3d-95e7e74f9450"),
            "phone",
            new PhoneData("16197363066", "Orange France", "MOBILE"),
            new FraudScore("54", "flag", FraudScoreLabel.Medium, "completed"),
            new SimSwap("failed", true, "Mobile Network Operator Not Supported"));

        internal static Result<FraudCheckRequest> BuildRequestWithFraudScore() =>
            FraudCheckRequest.Build()
                .WithPhone("447009000000")
                .WithFraudScore()
                .Create();
    }
}