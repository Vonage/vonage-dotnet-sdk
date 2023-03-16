using System;
using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class JwtTest
    {
        private const string ApplicationId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
        private readonly string PrivateKey = Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey");

        [Fact]
        public void GenerateToken_ShouldGenerateToken_GivenCredentialsAreProvided() =>
            new Jwt().GenerateToken(Credentials.FromAppIdAndPrivateKey(ApplicationId, PrivateKey)).Should()
                .NotBeEmpty();

        [Fact]
        public void GenerateToken_ShouldGenerateToken_GivenIdAndKeyAreProvided() =>
            new Jwt().GenerateToken(ApplicationId, PrivateKey).Should().NotBeEmpty();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GenerateToken_ShouldThrowAuthenticationException_GivenAppIdIsNullOrWhitespace(string key)
        {
            Action act = () => new Jwt().GenerateToken(ApplicationId, key);
            act.Should().ThrowExactly<VonageAuthenticationException>()
                .WithMessage("AppId or Private Key Path missing.");
        }

        [Fact]
        public void TestJwt()
        {
            var token = Jwt.CreateToken(ApplicationId, PrivateKey);
            Assert.False(string.IsNullOrEmpty(token));
        }
    }
}