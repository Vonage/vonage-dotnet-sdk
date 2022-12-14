using System.Net;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Xunit;

namespace Vonage.Video.Beta.Test.Common.Failures
{
    public class HttpFailureTest
    {
        [Fact]
        public void Test()
        {
            HttpFailure.From(HttpStatusCode.NotFound, "Some message").GetFailureMessage().Should()
                .Be("404 - Some message.");
        }
    }
}