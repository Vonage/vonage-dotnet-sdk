using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.Request;
using Vonage.Video.Authentication;
using Xunit;

namespace Vonage.Test.Unit.Video.Authentication
{
    public class VideoTokenGeneratorTest
    {
        private const string ApplicationId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
        private readonly string privateKey = TokenHelper.GetKey();

        [Fact]
        public void GenerateToken_ShouldReturnFailure_GivenTokenGenerationFails() =>
            new VideoTokenGenerator().GenerateToken(Credentials.FromAppIdAndPrivateKey("123", "random"), CreateClaims())
                .Should()
                .BeFailure(failure =>
                    failure.GetFailureMessage().Should()
                        .Be("RsaUsingSha alg expects key to be of RSA type or Jwk type with kty='RSA'"));

        [Fact]
        public void GenerateToken_ShouldReturnSuccess_GivenCredentialsAreProvided() =>
            new VideoTokenGenerator().GenerateToken(this.GetCredentials(), CreateClaims())
                .Should()
                .BeSuccess(token =>
                {
                    token.SessionId.Should().Be("sessionValue");
                    token.Token.Should().NotBeEmpty();
                });

        private static Result<TokenAdditionalClaims> CreateClaims() =>
            TokenAdditionalClaims
                .Parse("sessionValue");

        private Credentials GetCredentials() => Credentials.FromAppIdAndPrivateKey(ApplicationId, this.privateKey);
    }
}