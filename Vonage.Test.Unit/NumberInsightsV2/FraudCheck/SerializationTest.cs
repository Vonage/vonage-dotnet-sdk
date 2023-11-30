using System;
using Vonage.Common;
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

        public static FraudCheckResponse GetExpectedFraudCheckResponse() => new FraudCheckResponse(
            new Guid("6cb4c489-0fc8-4c40-8c3d-95e7e74f9450"),
            "phone",
            new PhoneData("16197363066", "Orange France", "MOBILE"),
            new FraudScore("54", "flag", "medium", "completed"),
            new SimSwap("failed", true, "Mobile Network Operator Not Supported"));

        [Fact]
        public void ShouldDeserialize200() => this.helper.Serializer
            .DeserializeObject<FraudCheckResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(GetExpectedFraudCheckResponse());

        [Fact]
        public void ShouldSerializeWithFraudScore() => FraudCheckRequest.Build()
            .WithPhone("447009000000")
            .WithFraudScore()
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithFraudScoreAndSimSwap() => FraudCheckRequest.Build()
            .WithPhone("447009000000")
            .WithFraudScore()
            .WithSimSwap()
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithSimSwap() => FraudCheckRequest.Build()
            .WithPhone("447009000000")
            .WithSimSwap()
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    }
}