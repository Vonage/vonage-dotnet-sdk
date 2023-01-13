using System.Net;
using FluentAssertions;
using Vonage.Server.Common.Failures;
using Xunit;

namespace Vonage.Server.Test.Common.Failures
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
    }
}