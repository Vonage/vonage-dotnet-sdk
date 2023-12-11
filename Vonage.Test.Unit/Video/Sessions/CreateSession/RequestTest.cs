using FluentAssertions;
using Vonage.Video.Sessions;
using Vonage.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Test.Unit.Video.Sessions.CreateSession
{
    public class RequestTest
    {
        [Fact]
        public void Default_ShouldReturnRequest()
        {
            CreateSessionRequest.Default.Location.Should().Be(IpAddress.Empty);
            CreateSessionRequest.Default.MediaMode.Should().Be(MediaMode.Relayed);
            CreateSessionRequest.Default.ArchiveMode.Should().Be(ArchiveMode.Manual);
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateSessionRequest.Default.GetEndpointPath()
                .Should()
                .Be("/session/create");
    }
}