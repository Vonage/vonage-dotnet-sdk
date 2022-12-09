using FluentAssertions;
using Vonage.Video.Beta.Common;
using Xunit;

namespace Vonage.Video.Beta.Test.Common
{
    public class ResultFailureTest
    {
        [Fact]
        public void FromError_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").Error.Should().Be("Some error.");
    }
}