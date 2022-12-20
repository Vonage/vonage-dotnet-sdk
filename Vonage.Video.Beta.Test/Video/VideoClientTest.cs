using AutoFixture;
using FluentAssertions;
using Vonage.Request;
using Vonage.Video.Beta.Video;
using Xunit;

namespace Vonage.Video.Beta.Test.Video
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
        public void Credentials_ShouldOverrideCredentials()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(this.fixture.Create<Credentials>())
            {
                Credentials = credentials,
            };
            client.Credentials.Should().Be(credentials);
        }

        [Fact]
        public void Constructor_ShouldInitializeSessionClient()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(credentials);
            client.SessionClient.Should().NotBeNull();
        }

        [Fact]
        public void Credentials_ShouldOverrideClients_GivenCredentialsAreProvided()
        {
            var client = new VideoClient(this.fixture.Create<Credentials>());
            var sessionClient = client.SessionClient;
            client.Credentials = this.fixture.Create<Credentials>();
            client.SessionClient.Should().NotBe(sessionClient);
            client.SignalingClient.Should().NotBe(sessionClient);
        }

        [Fact]
        public void Credentials_ShouldNotOverrideClients_GivenCredentialsAreNull()
        {
            var client = new VideoClient(this.fixture.Create<Credentials>());
            var sessionClient = client.SessionClient;
            var signalingClient = client.SignalingClient;
            client.Credentials = null;
            client.SessionClient.Should().Be(sessionClient);
            client.SignalingClient.Should().Be(signalingClient);
        }
    }
}