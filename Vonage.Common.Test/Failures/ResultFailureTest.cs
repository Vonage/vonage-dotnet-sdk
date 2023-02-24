using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;

namespace Vonage.Common.Test.Failures
{
    public class ResultFailureTest
    {
        [Fact]
        public void FromErrorMessage_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").GetFailureMessage().Should().Be("Some error.");

        [Fact]
        public void ToResult_ShouldReturnFailure() =>
            ResultFailure.ToResult<int>("Some error.").Should()
                .BeFailure(failure => failure.GetFailureMessage().Should().Be("Some error."));
    }
}