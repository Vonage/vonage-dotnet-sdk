using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.SimSwap.Authenticate;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.Authenticate;

[Trait("Category", "Request")]
public class AuthenticateRequestTest
{
    private const string ValidScope = "scope=test";
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        AuthenticateRequest.Parse(value, ValidScope).Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenScopeIsNullOrWhitespace(string value) =>
        AuthenticateRequest.Parse("1234567", value).Should()
            .BeParsingFailure("Scope cannot be null or whitespace.");
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberContainsNonDigits() =>
        AuthenticateRequest.Parse("1234567abc123", ValidScope).Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number can only contain digits."));
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsHigherThan7() =>
        AuthenticateRequest.Parse("123456", ValidScope).Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be lower than 7."));
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsLowerThan15() =>
        AuthenticateRequest.Parse("1234567890123456", ValidScope).Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be higher than 15."));
    
    [Theory]
    [InlineData("1234567", "1234567")]
    [InlineData("123456789012345", "123456789012345")]
    [InlineData("+1234567890", "1234567890")]
    [InlineData("+123456789012345", "123456789012345")]
    [InlineData("+++1234567890", "1234567890")]
    public void Parse_ShouldSetNumber(string value, string expected) =>
        AuthenticateRequest.Parse(value, ValidScope)
            .Map(request => request.PhoneNumber.Number)
            .Should()
            .BeSuccess(expected);
    
    [Fact]
    public void Parse_ShouldSetScope() =>
        AuthenticateRequest.Parse("1234567", ValidScope)
            .Map(request => request.Scope)
            .Should()
            .BeSuccess(ValidScope);
    
    [Fact]
    public void BuildAuthorizeRequest()
    {
        var request = AuthenticateRequest.Parse("123456789", ValidScope).GetSuccessUnsafe();
        request.BuildAuthorizeRequest().Number.Should().Be(request.PhoneNumber);
    }
}