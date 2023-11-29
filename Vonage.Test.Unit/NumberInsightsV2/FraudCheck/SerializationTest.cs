using System;
using Vonage.Common;
using Vonage.Common.Test;
using Xunit;

namespace Vonage.Test.Unit.NumberInsightsV2.FraudCheck
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200WithFraudScoreAndSimSwap() => throw new NotImplementedException();
    }
}