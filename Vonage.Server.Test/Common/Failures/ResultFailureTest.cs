using FluentAssertions;
using Vonage.Server.Common.Failures;
using Xunit;

namespace Vonage.Server.Test.Common.Failures
{
    public class ResultFailureTest
    {
        [Fact]
        public void FromError_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").GetFailureMessage().Should().Be("Some error.");
    }
}