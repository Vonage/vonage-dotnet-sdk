using FluentAssertions;
using Vonage.Common.Exceptions;
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
        public void ToException_ShouldReturnVonageException()
        {
            Action act = () => throw ResultFailure.FromErrorMessage("Some error.").ToException();
            act.Should().ThrowExactly<VonageException>().WithMessage("Some error.");
        }

        [Fact]
        public void ToResult_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").ToResult<int>().Should()
                .BeFailure(ResultFailure.FromErrorMessage("Some error."));
    }
}