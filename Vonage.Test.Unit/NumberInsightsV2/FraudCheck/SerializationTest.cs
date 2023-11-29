using System;
using Vonage.Common;
using Vonage.Common.Test;
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
        public void ShouldSerializeWithFraudScoreAndSimSwap() => throw new NotImplementedException();
    }
}