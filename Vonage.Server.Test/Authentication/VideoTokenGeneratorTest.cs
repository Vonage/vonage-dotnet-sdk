using System;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Vonage.Server.Authentication;
using Xunit;

namespace Vonage.Server.Test.Authentication
{
    public class VideoTokenGeneratorTest
    {
        private const string ApplicationId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
        private readonly string privateKey = Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey");

        [Fact]
        public void GenerateToken_ShouldReturnFailure_GivenTokenGenerationFails() =>
            CreateClaims()
                .Bind(claims =>
                    new VideoTokenGenerator().GenerateToken(Credentials.FromAppIdAndPrivateKey("123", "random"),
                        claims))
                .Should()
                .BeFailure(failure =>
                    failure.GetFailureMessage().Should()
                        .Be("RsaUsingSha alg expects key to be of RSA type or Jwk type with kty='RSA'"));

        [Fact]
        public void GenerateToken_ShouldReturnSuccess_GivenCredentialsAreProvided() =>
            CreateClaims()
                .Bind(claims => new VideoTokenGenerator().GenerateToken(this.GetCredentials(), claims))
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