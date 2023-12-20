using FluentAssertions;
using Vonage.Meetings.GetDialNumbers;
using Xunit;

namespace Vonage.Test.Meetings.GetDialNumbers
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            new GetDialNumbersRequest()
                .GetEndpointPath()
                .Should()
                .Be("/v1/meetings/dial-in-numbers");
    }
}