using Vonage.Common.Failures;
using Vonage.SimSwap.Check;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.Check;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        CheckRequest.Build()
            .WithPhoneNumber(value)
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberContainsNonDigits() =>
        CheckRequest.Build()
            .WithPhoneNumber("1234567abc123")
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number can only contain digits."));
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberLengthIsHigherThan7() =>
        CheckRequest.Build()
            .WithPhoneNumber("123456")
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be lower than 7."));
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberLengthIsLowerThan15() =>
        CheckRequest.Build()
            .WithPhoneNumber("1234567890123456")
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be higher than 15."));
    
    [Theory]
    [InlineData("1234567", "1234567")]
    [InlineData("123456789012345", "123456789012345")]
    [InlineData("+1234567890", "1234567890")]
    [InlineData("+123456789012345", "123456789012345")]
    [InlineData("+++1234567890", "1234567890")]
    public void Build_ShouldSetPhoneNumber_GivenNumberIsValid(string value, string expected) =>
        CheckRequest.Build()
            .WithPhoneNumber(value)
            .Create()
            .Map(number => number.PhoneNumber.Number)
            .Should()
            .BeSuccess(expected);
    
    [Theory]
    [InlineData(1)]
    [InlineData(2400)]
    public void Build_ShouldSetPeriod(int value) =>
        CheckRequest.Build()
            .WithPhoneNumber("1234567")
            .WithPeriod(value)
            .Create()
            .Map(number => number.Period)
            .Should()
            .BeSuccess(value);
    
    [Fact]
    public void Build_ShouldSetPeriodToDefault_GivenPeriodIsNotSet() =>
        CheckRequest.Build()
            .WithPhoneNumber("1234567")
            .Create()
            .Map(number => number.Period)
            .Should()
            .BeSuccess(240);
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Build_ShouldReturnFailure_GivenPeriodIsLowerThan15(int incorrectValue) =>
        CheckRequest.Build()
            .WithPhoneNumber("1234567")
            .WithPeriod(incorrectValue)
            .Create()
            .Should()
            .BeParsingFailure("Period cannot be lower than 1.");
    
    [Theory]
    [InlineData(2401)]
    [InlineData(2402)]
    public void Build_ShouldReturnFailure_GivenPeriodIsHigherThan2400(int incorrectValue) =>
        CheckRequest.Build()
            .WithPhoneNumber("1234567")
            .WithPeriod(incorrectValue)
            .Create()
            .Should()
            .BeParsingFailure("Period cannot be higher than 2400.");
}