using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Tokens;
using Vonage.Video.Beta.Test.Extensions;
using Xunit;

namespace Vonage.Video.Beta.Test.Common.Tokens
{
    public class TokenAdditionalInformationTest
    {
        private readonly string sessionId;
        private readonly string scope;
        private readonly Role role;

        public TokenAdditionalInformationTest()
        {
            var fixture = new Fixture();
            this.sessionId = fixture.Create<string>();
            this.scope = fixture.Create<string>();
            this.role = fixture.Create<Role>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            TokenAdditionalClaims.Parse(value, this.scope, this.role)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenMandatoryValuesAreProvided() =>
            TokenAdditionalClaims.Parse(this.sessionId)
                .Should()
                .BeSuccess(request =>
                {
                    request.SessionId.Should().Be(this.sessionId);
                    request.Role.Should().Be(Role.Publisher);
                    request.Scope.Should().Be(TokenAdditionalClaims.DefaultScope);
                });

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            TokenAdditionalClaims.Parse(this.sessionId, this.scope, this.role)
                .Should()
                .BeSuccess(request =>
                {
                    request.SessionId.Should().Be(this.sessionId);
                    request.Role.Should().Be(this.role);
                    request.Scope.Should().Be(this.scope);
                });
    }
}