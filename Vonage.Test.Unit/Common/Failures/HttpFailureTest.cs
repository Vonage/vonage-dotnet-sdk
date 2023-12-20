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
            HttpFailure.From(HttpStatusCode.NotFound, "Some message", "json data").GetFailureMessage().Should()
                .Be("404 - Some message - json data.");

        [Fact]
        public void ToException_ShouldReturnVonageException()
        {
            var exception = HttpFailure.From(HttpStatusCode.NotFound, "Some message", "json data").ToException()
                .Should().BeOfType<VonageHttpRequestException>().Which;
            exception.Message.Should().Be("Some message");
            exception.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
            exception.Json.Should().Be("json data");
        }

        [Fact]
        public void Type_ShouldReturnHttpFailure() => HttpFailure.From(HttpStatusCode.NotFound)
            .Type
            .Should()
            .Be(typeof(HttpFailure));
    }
}