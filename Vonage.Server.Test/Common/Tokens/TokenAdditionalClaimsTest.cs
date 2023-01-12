using AutoFixture;
using EnumsNET;
using FluentAssertions;
using Vonage.Server.Common.Failures;
using Vonage.Server.Common.Tokens;
using Vonage.Server.Test.Extensions;
using Xunit;

namespace Vonage.Server.Test.Common.Tokens
{
    public class TokenAdditionalInformationTest
    {
        private readonly Role role;
        private readonly string scope;
        private readonly string sessionId;

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
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            TokenAdditionalClaims.Parse(this.sessionId, this.scope, this.role)
                .Should()
                .BeSuccess(request =>
                {
                    request.SessionId.Should().Be(this.sessionId);
                    request.Role.Should().Be(this.role);
                    request.Scope.Should().Be(this.scope);
                });

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
        public void ToDataDictionary_ShouldReturnDictionaryWithValues() =>
            TokenAdditionalClaims.Parse(this.sessionId, this.scope, this.role)
                .Map(claims => claims.ToDataDictionary())
                .Should()
                .BeSuccess(request =>
                {
                    request["session_id"].Should().Be(this.sessionId);
                    request["role"].Should().Be(this.role.AsString(EnumFormat.Description));
                    request["scope"].Should().Be(this.scope);
                });
    }
}