using System.Collections.Generic;
using AutoFixture;
using EnumsNET;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Authentication;
using Xunit;

namespace Vonage.Test.Video.Authentication;

[Trait("Category", "Unit")]
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
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Theory]
    [InlineData(TokenAdditionalClaims.ReservedClaimSessionId)]
    [InlineData(TokenAdditionalClaims.ReservedClaimRole)]
    [InlineData(TokenAdditionalClaims.ReservedClaimScope)]
    [InlineData(Jwt.ReservedClaimApplicationId)]
    [InlineData(Jwt.ReservedClaimIssuedAt)]
    [InlineData(Jwt.ReservedClaimTokenId)]
    public void Parse_ShouldReturnFailure_GivenClaimKeyIsReserved(string invalidKey) =>
        TokenAdditionalClaims.Parse(this.sessionId, claims: new Dictionary<string, object>
            {
                {invalidKey, 0},
            })
            .Should()
            .BeParsingFailure($"Claims key '{invalidKey}' is reserved.");

    [Fact]
    public void Parse_ShouldSetClaims() =>
        TokenAdditionalClaims.Parse(this.sessionId, claims: new Dictionary<string, object> {{"id", 1}})
            .Map(token => token.Claims)
            .Should()
            .BeSuccess(claims => claims.Should().BeEquivalentTo(new Dictionary<string, object> {{"id", 1}}));

    [Fact]
    public void Parse_ShouldSetSessionId() =>
        TokenAdditionalClaims.Parse(this.sessionId)
            .Map(token => token.SessionId)
            .Should()
            .BeSuccess(this.sessionId);

    [Fact]
    public void Parse_ShouldSetRole() =>
        TokenAdditionalClaims.Parse(this.sessionId, role: Role.Subscriber)
            .Map(token => token.Role)
            .Should()
            .BeSuccess(Role.Subscriber);

    [Fact]
    public void Parse_ShouldSetScope() =>
        TokenAdditionalClaims.Parse(this.sessionId, "custom_scope")
            .Map(token => token.Scope)
            .Should()
            .BeSuccess("custom_scope");

    [Fact]
    public void Parse_ShouldHaveRole_GivenDefault() =>
        TokenAdditionalClaims.Parse(this.sessionId)
            .Map(token => token.Role)
            .Should()
            .BeSuccess(Role.Publisher);

    [Fact]
    public void Parse_ShouldHaveScope_GivenDefault() =>
        TokenAdditionalClaims.Parse(this.sessionId)
            .Map(token => token.Scope)
            .Should()
            .BeSuccess(TokenAdditionalClaims.DefaultScope);

    [Fact]
    public void Parse_ShouldHaveEmptyClaims_GivenDefault() =>
        TokenAdditionalClaims.Parse(this.sessionId)
            .Map(token => token.Claims)
            .Should()
            .BeSuccess(claims => claims.Should().BeEmpty());

    [Fact]
    public void ToDataDictionary_ShouldReturnDictionaryWithValues() =>
        TokenAdditionalClaims.Parse(this.sessionId, this.scope, this.role,
                new Dictionary<string, object> {{"id", 1}, {"custom", "value"}})
            .Map(claims => claims.ToDataDictionary())
            .Should()
            .BeSuccess(request =>
            {
                request["session_id"].Should().Be(this.sessionId);
                request["role"].Should().Be(this.role.AsString(EnumFormat.Description));
                request["scope"].Should().Be(this.scope);
                request["id"].Should().Be(1);
                request["custom"].Should().Be("value");
            });
}