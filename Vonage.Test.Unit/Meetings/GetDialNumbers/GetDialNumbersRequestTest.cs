using FluentAssertions;
using Vonage.Meetings.GetDialNumbers;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetDialNumbers
{
    public class GetDialNumbersRequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            new GetDialNumbersRequest()
                .GetEndpointPath()
                .Should()
                .Be("/beta/meetings/dial-in-numbers");
    }
}