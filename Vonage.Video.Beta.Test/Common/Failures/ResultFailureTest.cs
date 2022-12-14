using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Xunit;

namespace Vonage.Video.Beta.Test.Common.Failures
{
    public class ResultFailureTest
    {
        [Fact]
        public void FromError_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").GetFailureMessage().Should().Be("Some error.");
    }
}