using FluentAssertions;
using Vonage.Common.Failures;

namespace Vonage.Common.Test.Failures
{
    public class ResultFailureTest
    {
        [Fact]
        public void FromError_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").GetFailureMessage().Should().Be("Some error.");
    }
}