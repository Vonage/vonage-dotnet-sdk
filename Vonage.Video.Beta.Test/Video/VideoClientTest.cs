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
            client.SessionClient.Credentials.Should().Be(credentials);
        }

        [Fact]
        public void Credentials_ShouldOverrideSessionClient()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(this.fixture.Create<Credentials>())
            {
                Credentials = credentials,
            };
            client.SessionClient.Credentials.Should().Be(credentials);
        }

        [Fact]
        public void Credentials_ShouldNotUpdateCredentials_GivenCredentialsAreNull()
        {
            var credentials = this.fixture.Create<Credentials>();
            var client = new VideoClient(credentials)
            {
                Credentials = null,
            };
            client.Credentials.Should().Be(credentials);
            client.SessionClient.Credentials.Should().Be(credentials);
        }
    }
}