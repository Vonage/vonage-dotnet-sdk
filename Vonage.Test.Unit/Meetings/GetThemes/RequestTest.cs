using FluentAssertions;
using Vonage.Meetings.GetThemes;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetThemes
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            new GetThemesRequest()
                .GetEndpointPath()
                .Should()
                .Be("/v1/meetings/themes");
    }
}