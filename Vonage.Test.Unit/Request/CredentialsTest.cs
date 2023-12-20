using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Request;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Test.Unit.Common.TestHelpers;
using Xunit;

namespace Vonage.Test.Unit.Request
{
    public class CredentialsTest
    {
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
            TokenHelper.GetKey());
    }
}