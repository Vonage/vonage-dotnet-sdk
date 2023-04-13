using System.Collections.Generic;
using FsCheck;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit.Request
{
    public class CredentialsTest
    {
        [Fact]
        public void GetPreferredAuthenticationType_ReturnsBearer_GivenContainsApplicationIdAndPrivateKey() =>
            Credentials.FromAppIdAndPrivateKey("appId", "privateKey")
                .GetPreferredAuthenticationType()
                .Should()
                .BeSuccess(AuthType.Bearer);
        
        [Fact]
        public void GetPreferredAuthenticationType_ReturnsBasic_GivenContainsApiKeyAndApiSecret() =>
            Credentials.FromApiKeyAndSecret("apiKey", "apiSecret")
                .GetPreferredAuthenticationType()
                .Should()
                .BeSuccess(AuthType.Basic);
        
        [Theory]
        [MemberData(nameof(GetInvalidCredentials))]
        public void GetPreferredAuthenticationType_ReturnsNone_GivenInformationIsMissing(Credentials credentials) =>
            credentials.GetPreferredAuthenticationType()
                .Should()
                .BeFailure(new AuthenticationFailure());
        
        public static IEnumerable<object[]> GetInvalidCredentials() {
            yield return new object[] { Credentials.FromApiKeyAndSecret("", "apiSecret") };
            yield return new object[] { Credentials.FromApiKeyAndSecret(" ", "apiSecret")};
            yield return new object[] { Credentials.FromApiKeyAndSecret(null, "apiSecret")};
            yield return new object[] { Credentials.FromApiKeyAndSecret("apiKey", "")};
            yield return new object[] { Credentials.FromApiKeyAndSecret("apiKey", " ")};
            yield return new object[] { Credentials.FromApiKeyAndSecret("apiKey", null)};
            yield return new object[] { Credentials.FromAppIdAndPrivateKey("", "privateKey")};
            yield return new object[] { Credentials.FromAppIdAndPrivateKey(" ", "privateKey")};
            yield return new object[] { Credentials.FromAppIdAndPrivateKey(null, "privateKey")};
            yield return new object[] { Credentials.FromAppIdAndPrivateKey("applicationId", "")};
            yield return new object[] { Credentials.FromAppIdAndPrivateKey("applicationId", " ")};
            yield return new object[] { Credentials.FromAppIdAndPrivateKey("applicationId", null)};
        }
    }
}