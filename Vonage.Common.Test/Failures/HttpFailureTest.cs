using System.Net;
using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Common.Failures;

namespace Vonage.Common.Test.Failures
{
    public class HttpFailureTest
    {
        [Fact]
        public void GetFailureMessage_ShouldReturnMessage_GivenFailureIsCreatedFromCode() =>
            HttpFailure.From(HttpStatusCode.NotFound).GetFailureMessage().Should()
                .Be("404.");

        [Fact]
        public void GetFailureMessage_ShouldReturnMessage_GivenFailureIsCreatedFromCodeAndMessage() =>
            HttpFailure.From(HttpStatusCode.NotFound, "Some message").GetFailureMessage().Should()
                .Be("404 - Some message.");

        [Fact]
        public void ToException_ShouldReturnVonageException()
        {
            Action act = () => throw HttpFailure.From(HttpStatusCode.NotFound, "Some message").ToException();
            act.Should().ThrowExactly<VonageHttpRequestException>().WithMessage("Some message").And.HttpStatusCode
                .Should().Be(HttpStatusCode.NotFound);
        }
    }
}