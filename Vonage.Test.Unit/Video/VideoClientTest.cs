using AutoFixture;
using FluentAssertions;
using Vonage.Request;
using Vonage.Video;
using Xunit;

namespace Vonage.Test.Unit.Video
{
    public class VideoClientTest
    {
        private readonly Fixture fixture = new Fixture();

        [Fact]
        public void Constructor_ShouldAssignCredentials()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(credentials);
            client.Credentials.Should().Be(credentials);
        }

        [Fact]
        public void Constructor_ShouldInitializeClients()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(credentials);
            client.SessionClient.Should().NotBeNull();
            client.SignalingClient.Should().NotBeNull();
            client.ModerationClient.Should().NotBeNull();
            client.ArchiveClient.Should().NotBeNull();
            client.BroadcastClient.Should().NotBeNull();
        }

        [Fact]
        public void Credentials_ShouldOverrideCredentials()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(this.fixture.Create<Credentials>())
            {
                Credentials = credentials,
            };
            client.Credentials.Should().Be(credentials);
        }
    }
}