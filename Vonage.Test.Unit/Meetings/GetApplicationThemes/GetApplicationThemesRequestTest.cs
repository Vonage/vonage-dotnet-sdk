using FluentAssertions;
using Vonage.Meetings.GetApplicationThemes;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetApplicationThemes
{
    public class GetApplicationThemesRequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            new GetApplicationThemesRequest()
                .GetEndpointPath()
                .Should()
                .Be("/beta/meetings/themes");
    }
}