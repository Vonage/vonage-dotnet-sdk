using System;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class JwtTest
    {
        private const string ApplicationId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
        private readonly string privateKey = Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GenerateToken_ShouldReturnAuthenticationFailure_GivenPrivateKeyIsNullOrWhitespace(string key) =>
            new Jwt().GenerateToken(ApplicationId, key).Should().BeFailure(new AuthenticationFailure());

        [Fact]
        public void GenerateToken_ShouldReturnSuccess_GivenCredentialsAreProvided() =>
            new Jwt().GenerateToken(Credentials.FromAppIdAndPrivateKey(ApplicationId, this.privateKey)).Should()
                .BeSuccess(success => success.Should().NotBeEmpty());

        [Fact]
        public void GenerateToken_ShouldReturnSuccess_GivenIdAndKeyAreProvided() =>
            new Jwt().GenerateToken(ApplicationId, this.privateKey).Should()
                .BeSuccess(success => success.Should().NotBeEmpty());

        [Fact]
        public void TestJwt()
        {
            var token = Jwt.CreateToken(ApplicationId, this.privateKey);
            Assert.False(string.IsNullOrEmpty(token));
        }
    }
}