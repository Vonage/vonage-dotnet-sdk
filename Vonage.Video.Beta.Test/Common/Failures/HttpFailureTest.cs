using System.Net;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Xunit;

namespace Vonage.Video.Beta.Test.Common.Failures
{
    public class HttpFailureTest
    {
        [Fact]
        public void GetFailureMessage_ShouldReturnMessage_GivenFailureIsCreatedFromCodeAndMessage() =>
            HttpFailure.From(HttpStatusCode.NotFound, "Some message").GetFailureMessage().Should()
                .Be("404 - Some message.");

        [Fact]
        public void GetFailureMessage_ShouldReturnMessage_GivenFailureIsCreatedFromCode() =>
            HttpFailure.From(HttpStatusCode.NotFound).GetFailureMessage().Should()
                .Be("404.");
    }
}