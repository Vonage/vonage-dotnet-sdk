using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Vonage.Test.Unit
{
    public class ConfigurationTest
    {
        [Fact]
        public void FromConfiguration_ShouldLoadEmptyCredentials_GivenConfigurationContainsNoElement()
        {
            var credentials = Configuration.FromConfiguration(new ConfigurationBuilder().Build());
            credentials.ApiKey.Should().BeEmpty();
            credentials.ApiSecret.Should().BeEmpty();
            credentials.ApplicationId.Should().BeEmpty();
            credentials.ApplicationKey.Should().BeEmpty();
            credentials.SecuritySecret.Should().BeEmpty();
            credentials.SigningMethod.Should().BeEmpty();
            credentials.UserAgent.Should().BeEmpty();
            credentials.EuropeApiUrl.Should().Be(new Uri("https://api-eu.vonage.com"));
            credentials.NexmoApiUrl.Should().Be(new Uri("https://api.nexmo.com"));
            credentials.RestApiUrl.Should().Be(new Uri("https://rest.nexmo.com"));
            credentials.VideoApiUrl.Should().Be(new Uri("https://video.api.vonage.com"));
        }
    }
}