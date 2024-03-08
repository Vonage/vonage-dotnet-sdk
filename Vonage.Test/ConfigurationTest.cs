﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Vonage.Cryptography;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test
{
    [Trait("Category", "Unit")]
    public class ConfigurationTest
    {
        [Fact]
        public void BuildCredentials_ShouldCreateEmptyCredentials_GivenConfigurationContainsNoElement()
        {
            var credentials = Configuration.FromConfiguration(new ConfigurationBuilder().Build()).BuildCredentials();
            credentials.ApiKey.Should().BeEmpty();
            credentials.ApiSecret.Should().BeEmpty();
            credentials.ApplicationId.Should().BeEmpty();
            credentials.ApplicationKey.Should().BeEmpty();
            credentials.SecuritySecret.Should().BeEmpty();
            credentials.Method.Should().Be(SmsSignatureGenerator.Method.md5hash);
        }

        [Fact]
        public void FromConfiguration_ShouldCreateEmptyConfiguration_GivenConfigurationContainsNoElement()
        {
            var configuration = Configuration.FromConfiguration(new ConfigurationBuilder().Build());
            configuration.ApiKey.Should().BeEmpty();
            configuration.ApiSecret.Should().BeEmpty();
            configuration.ApplicationId.Should().BeEmpty();
            configuration.ApplicationKey.Should().BeEmpty();
            configuration.SecuritySecret.Should().BeEmpty();
            configuration.SigningMethod.Should().BeEmpty();
            configuration.UserAgent.Should().BeEmpty();
            configuration.EuropeApiUrl.Should().Be(new Uri("https://api-eu.vonage.com"));
            configuration.VonageUrls.Nexmo.Should().Be(new Uri("https://api.nexmo.com"));
            configuration.VonageUrls.Rest.Should().Be(new Uri("https://rest.nexmo.com"));
            configuration.VonageUrls.Video.Should().Be(new Uri("https://video.api.vonage.com"));
            configuration.RequestTimeout.Should().BeNone();
        }

        [Fact]
        public void FromConfiguration_ShouldSetApiKey_GivenConfigurationContainsApiKey() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage_key", "RandomValue"},
                })
                .Build()).ApiKey.Should().Be("RandomValue");

        [Fact]
        public void FromConfiguration_ShouldSetApiKSecret_GivenConfigurationContainsApiSecret() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage_secret", "RandomValue"},
                })
                .Build()).ApiSecret.Should().Be("RandomValue");

        [Fact]
        public void FromConfiguration_ShouldSetApplicationId_GivenConfigurationContainsApplicationId() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Application.Id", "RandomValue"},
                })
                .Build()).ApplicationId.Should().Be("RandomValue");

        [Fact]
        public void FromConfiguration_ShouldSetApplicationKey_GivenConfigurationContainsApplicationKey() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Application.Key", "RandomValue"},
                })
                .Build()).ApplicationKey.Should().Be("RandomValue");

        [Fact]
        public void FromConfiguration_ShouldSetEuropeApiUrl_GivenConfigurationContainsEuropeApiUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Url.Api.Europe", "https://api.vonage.com"},
                })
                .Build()).EuropeApiUrl.Should().Be(new Uri("https://api.vonage.com"));

        [Fact]
        public void FromConfiguration_ShouldSetNexmoApiUrl_GivenConfigurationContainsNexmoApiUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Url.Api", "https://api.vonage.com"},
                })
                .Build()).VonageUrls.Nexmo.Should().Be(new Uri("https://api.vonage.com"));

        [Fact]
        public void FromConfiguration_ShouldSetRequestTimeout_GivenConfigurationContainsRequestTimeout() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.RequestTimeout", "100"},
                })
                .Build()).RequestTimeout.Should().BeSome(TimeSpan.FromSeconds(100));

        [Fact]
        public void FromConfiguration_ShouldSetRestApiUrl_GivenConfigurationContainsRestApiUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Url.Rest", "https://api.vonage.com"},
                })
                .Build()).VonageUrls.Rest.Should().Be(new Uri("https://api.vonage.com"));

        [Fact]
        public void FromConfiguration_ShouldSetSecuritySecret_GivenConfigurationContainsSecuritySecret() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.security_secret", "RandomValue"},
                })
                .Build()).SecuritySecret.Should().Be("RandomValue");

        [Fact]
        public void FromConfiguration_ShouldSetSigningMethod_GivenConfigurationContainsSigningMethod() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.signing_method", "sha512"},
                })
                .Build()).SigningMethod.Should().Be("sha512");

        [Fact]
        public void FromConfiguration_ShouldSetUserAgent_GivenConfigurationContainsUserAgent() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.UserAgent", "RandomValue"},
                })
                .Build()).UserAgent.Should().Be("RandomValue");

        [Fact]
        public void FromConfiguration_ShouldSetVideoApiUrl_GivenConfigurationContainsVideoApiUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Url.Api.Video", "https://api.vonage.com"},
                })
                .Build()).VonageUrls.Video.Should().Be(new Uri("https://api.vonage.com"));

        [Theory]
        [InlineData(VonageUrls.Region.US, "appSettings:Vonage.Url.Api.Us")]
        [InlineData(VonageUrls.Region.EU, "appSettings:Vonage.Url.Api.Eu")]
        [InlineData(VonageUrls.Region.APAC, "appSettings:Vonage.Url.Api.Apac")]
        public void VonageUrl_ShouldReturnCustomApiUsUrl_GivenConfigurationContainsApiUsUrl(VonageUrls.Region region,
            string key) =>
            Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        {key, "https://api.com"},
                    }).Build())
                .VonageUrls.Get(region).Should().Be(new Uri("https://api.com"));

        [Fact]
        public void VonageUrl_ShouldReturnCustomNexmoUrl_GivenConfigurationContainsDefaultUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                        {{"appSettings:Vonage.Url.Api", "https://api.com"}}).Build())
                .VonageUrls.Nexmo.Should().Be(new Uri("https://api.com"));

        [Fact]
        public void VonageUrl_ShouldReturnCustomRestUrl_GivenConfigurationContainsRestUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        {"appSettings:Vonage.Url.Rest", "https://api.com"},
                    }).Build())
                .VonageUrls.Rest.Should().Be(new Uri("https://api.com"));

        [Fact]
        public void VonageUrl_ShouldReturnCustomVideoUrl_GivenConfigurationContainsVideoUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        {"appSettings:Vonage.Url.Api.Video", "https://api.com"},
                    }).Build())
                .VonageUrls.Video.Should().Be(new Uri("https://api.com"));

        [Theory]
        [InlineData(VonageUrls.Region.US, "https://api-us.vonage.com")]
        public void VonageUrl_ShouldReturnDefaultApiUsUrl(VonageUrls.Region region, string defaultValue) =>
            Configuration.FromConfiguration(new ConfigurationBuilder().Build())
                .VonageUrls.Get(region).Should().Be(new Uri(defaultValue));

        [Fact]
        public void VonageUrl_ShouldReturnDefaultRestUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder().Build())
                .VonageUrls.Rest.Should().Be(new Uri("https://rest.nexmo.com"));

        [Fact]
        public void VonageUrl_ShouldReturnDefaultVideoUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder().Build())
                .VonageUrls.Video.Should().Be(new Uri("https://video.api.vonage.com"));

        [Fact]
        public void VonageUrl_ShouldReturnNexmoUrl() =>
            Configuration.FromConfiguration(new ConfigurationBuilder().Build())
                .VonageUrls.Nexmo.Should().Be(new Uri("https://api.nexmo.com"));
    }
}