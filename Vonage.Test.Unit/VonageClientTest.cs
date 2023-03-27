using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class VonageClientTest
    {
        private readonly Credentials credentials;
        private readonly Fixture fixture;
        private readonly VonageClient client;

        public VonageClientTest()
        {
            this.fixture = new Fixture();
            this.credentials = this.fixture.Create<Credentials>();
            this.client = new VonageClient(this.credentials);
        }

        [Fact]
        public void Constructor_ShouldAssignCredentials() => this.client.Credentials.Should().Be(this.credentials);

        [Fact]
        public void Credentials_ShouldOverrideCredentials()
        {
            var newCredentials = this.fixture.Create<Credentials>();
            this.client.Credentials = newCredentials;
            this.client.Credentials.Should().Be(newCredentials);
        }

        [Fact]
        public void Credentials_ShouldThrowArgumentNullException_GivenCredentialsAreNull()
        {
            Action act = () => this.client.Credentials = null;
            act.Should().Throw<ArgumentNullException>().WithParameterName(nameof(this.client.Credentials));
        }
    }
}