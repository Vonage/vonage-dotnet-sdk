using AutoFixture;
using FluentAssertions;
using Vonage.Request;
using Vonage.Server.Video;
using Xunit;

namespace Vonage.Server.Test.Video
{
    public class VideoClientTest
    {
        private readonly Fixture fixture;

        public VideoClientTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldAssignCredentials()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(credentials);
            client.Credentials.Should().Be(credentials);
        }

        [Fact]
        public void Constructor_ShouldInitializeSessionClient()
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
        public void Credentials_ShouldNotOverrideClients_GivenCredentialsAreNull()
        {
            var client = new VideoClient(this.fixture.Create<Credentials>());
            var sessionClient = client.SessionClient;
            var signalingClient = client.SignalingClient;
            var moderationClient = client.ModerationClient;
            var archiveClient = client.ArchiveClient;
            var broadcastClient = client.BroadcastClient;
            client.Credentials = null;
            client.SessionClient.Should().Be(sessionClient);
            client.SignalingClient.Should().Be(signalingClient);
            client.ModerationClient.Should().Be(moderationClient);
            client.ArchiveClient.Should().Be(archiveClient);
            client.BroadcastClient.Should().Be(broadcastClient);
        }

        [Fact]
        public void Credentials_ShouldOverrideClients_GivenCredentialsAreProvided()
        {
            var client = new VideoClient(this.fixture.Create<Credentials>());
            var sessionClient = client.SessionClient;
            var signalingClient = client.SignalingClient;
            var moderationClient = client.ModerationClient;
            var archiveClient = client.ArchiveClient;
            var broadcastClient = client.BroadcastClient;
            client.Credentials = this.fixture.Create<Credentials>();
            client.SessionClient.Should().NotBe(sessionClient);
            client.SignalingClient.Should().NotBe(signalingClient);
            client.ModerationClient.Should().NotBe(moderationClient);
            client.ArchiveClient.Should().NotBe(archiveClient);
            client.BroadcastClient.Should().NotBe(broadcastClient);
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