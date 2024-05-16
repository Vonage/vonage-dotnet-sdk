using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.Authenticate;

[Trait("Category", "Request")]
public class AuthenticateRequest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse(value).Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));
    
    [Fact]
    public void Parse_ShouldReturnNumberWithPlusIndicator() =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse("123456789")
            .Map(number => number.PhoneNumber.NumberWithInternationalIndicator)
            .Should()
            .BeSuccess("+123456789");
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberContainsNonDigits() =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse("1234567abc123").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number can only contain digits."));
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsHigherThan7() =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse("123456").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be lower than 7."));
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsLowerThan15() =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse("1234567890123456").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be higher than 15."));
    
    [Theory]
    [InlineData("1234567", "1234567")]
    [InlineData("123456789012345", "123456789012345")]
    public void Parse_ShouldReturnSuccess_GivenNumberIsValid(string value, string expected) =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse(value).Map(number => number.PhoneNumber.Number).Should()
            .BeSuccess(expected);
    
    [Theory]
    [InlineData("+1234567890", "1234567890")]
    [InlineData("+123456789012345", "123456789012345")]
    [InlineData("+++1234567890", "1234567890")]
    public void Parse_ShouldReturnSuccess_GivenNumberStartWithPlus(string value, string expected) =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse(value).Map(number => number.PhoneNumber.Number).Should()
            .BeSuccess(expected);
    
    [Fact]
    public void ToString_ShouldReturnNumber() =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse("123456789").Map(number => number.PhoneNumber.ToString())
            .Should()
            .BeSuccess("123456789");
    
    [Fact]
    public void BuildAuthorizeRequest()
    {
        var request = Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse("123456789").GetSuccessUnsafe();
        request.BuildAuthorizeRequest().Number.Should().Be(request.PhoneNumber);
    }
}