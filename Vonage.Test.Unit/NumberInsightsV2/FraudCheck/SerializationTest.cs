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

        public static FraudCheckResponse GetExpectedFraudCheckResponse() => throw new NotImplementedException();

        [Fact]
        public void ShouldDeserialize200() => throw new NotImplementedException();

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