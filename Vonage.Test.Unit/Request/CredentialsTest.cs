using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Cryptography;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit.Request
{
    public class CredentialsTest
    {
        [Fact]
        public void FromConfiguration_ShouldLoadEmptyCredentials_GivenConfigurationContainsNoElement()
        {
            var credentials = Credentials.FromConfiguration(new ConfigurationBuilder().Build());
            credentials.ApiKey.Should().BeNull();
            credentials.ApiSecret.Should().BeNull();
            credentials.ApplicationId.Should().BeNull();
            credentials.ApplicationKey.Should().BeNull();
            credentials.Method.Should().Be(SmsSignatureGenerator.Method.md5hash);
            credentials.SecuritySecret.Should().BeNull();
            credentials.AppUserAgent.Should().BeNull();
        }

        [Fact]
        public void FromConfiguration_ShouldSetApiKey_GivenConfigurationContainsApiKey() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage_key", "testKey"},
                })
                .Build()).ApiKey.Should().Be("testKey");

        [Fact]
        public void FromConfiguration_ShouldSetApiKSecret_GivenConfigurationContainsApiSecret() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage_secret", "testSecret"},
                })
                .Build()).ApiSecret.Should().Be("testSecret");

        [Fact]
        public void FromConfiguration_ShouldSetApplicationId_GivenConfigurationContainsApplicationId() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Application.Id", "testApplicationId"},
                })
                .Build()).ApplicationId.Should().Be("testApplicationId");

        [Fact]
        public void FromConfiguration_ShouldSetApplicationKey_GivenConfigurationContainsApplicationKey() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Application.Key", "testApplicationKey"},
                })
                .Build()).ApplicationKey.Should().Be("testApplicationKey");

        [Fact]
        public void FromConfiguration_ShouldSetDefaultSigningMethod_GivenConfigurationContainsInvalidSigningMethod() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.signing_method", "invalid"},
                })
                .Build()).Method.Should().Be(SmsSignatureGenerator.Method.md5hash);

        [Fact]
        public void FromConfiguration_ShouldSetSecuritySecret_GivenConfigurationContainsSecuritySecret() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.security_secret", "testSecuritySecret"},
                })
                .Build()).SecuritySecret.Should().Be("testSecuritySecret");

        [Fact]
        public void FromConfiguration_ShouldSetSigningMethod_GivenConfigurationContainsSigningMethod() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.signing_method", "sha512"},
                })
                .Build()).Method.Should().Be(SmsSignatureGenerator.Method.sha512);

        [Fact]
        public void FromConfiguration_ShouldSetUserAgent_GivenConfigurationContainsUserAgent() =>
            Credentials.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.UserAgent", "testUserAgent"},
                })
                .Build()).AppUserAgent.Should().Be("testUserAgent");

        [Fact]
        public void GetAuthenticationHeader_ReturnsBasicScheme_GivenContainsApiKeyAndApiSecret() =>
            BuildBasicCredentials()
                .GetAuthenticationHeader()
                .Map(header => header.Scheme)
                .Should()
                .BeSuccess("Basic");

        [Fact]
        public void GetAuthenticationHeader_ReturnsBearerScheme_GivenContainsApplicationIdAndPrivateKey() =>
            BuildBearerCredentials()
                .GetAuthenticationHeader()
                .Map(header => header.Scheme)
                .Should()
                .BeSuccess("Bearer");

        [Fact]
        public void GetAuthenticationHeader_ReturnsEncodedCredentials_GivenContainsApiKeyAndApiSecret() =>
            BuildBasicCredentials()
                .GetAuthenticationHeader()
                .Map(header => header.Parameter)
                .Should()
                .BeSuccess(Convert.ToBase64String(Encoding.UTF8.GetBytes("apiKey:apiSecret")));

        [Fact]
        public void GetAuthenticationHeader_ReturnsToken_GivenContainsApplicationIdAndPrivateKey() =>
            BuildBearerCredentials()
                .GetAuthenticationHeader()
                .Map(header => header.Parameter)
                .Should()
                .BeSuccess(success => success.Should().NotBeNullOrWhiteSpace());

        public static IEnumerable<object[]> GetInvalidCredentials()
        {
            yield return new object[] {Credentials.FromApiKeyAndSecret("", "apiSecret")};
            yield return new object[] {Credentials.FromApiKeyAndSecret(" ", "apiSecret")};
            yield return new object[] {Credentials.FromApiKeyAndSecret(null, "apiSecret")};
            yield return new object[] {Credentials.FromApiKeyAndSecret("apiKey", "")};
            yield return new object[] {Credentials.FromApiKeyAndSecret("apiKey", " ")};
            yield return new object[] {Credentials.FromApiKeyAndSecret("apiKey", null)};
            yield return new object[] {Credentials.FromAppIdAndPrivateKey("", "privateKey")};
            yield return new object[] {Credentials.FromAppIdAndPrivateKey(" ", "privateKey")};
            yield return new object[] {Credentials.FromAppIdAndPrivateKey(null, "privateKey")};
            yield return new object[] {Credentials.FromAppIdAndPrivateKey("applicationId", "")};
            yield return new object[] {Credentials.FromAppIdAndPrivateKey("applicationId", " ")};
            yield return new object[] {Credentials.FromAppIdAndPrivateKey("applicationId", null)};
        }

        [Fact]
        public void GetPreferredAuthenticationType_ReturnsBasic_GivenContainsApiKeyAndApiSecret() =>
            BuildBasicCredentials()
                .GetPreferredAuthenticationType()
                .Should()
                .BeSuccess(AuthType.Basic);

        [Fact]
        public void GetPreferredAuthenticationType_ReturnsBearer_GivenContainsApplicationIdAndPrivateKey() =>
            BuildBearerCredentials()
                .GetPreferredAuthenticationType()
                .Should()
                .BeSuccess(AuthType.Bearer);

        [Theory]
        [MemberData(nameof(GetInvalidCredentials))]
        public void GetPreferredAuthenticationType_ReturnsFailure_GivenInformationIsMissing(Credentials credentials) =>
            credentials.GetPreferredAuthenticationType()
                .Should()
                .BeFailure(new AuthenticationFailure());

        private static Credentials BuildBasicCredentials() => Credentials.FromApiKeyAndSecret("apiKey", "apiSecret");

        private static Credentials BuildBearerCredentials() => Credentials.FromAppIdAndPrivateKey("appId",
            Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey"));
    }
}