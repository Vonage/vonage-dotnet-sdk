using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;

namespace Vonage.Common.Test;

public class PhoneNumberTest
{
    [Fact]
    public void NumberWithInternationalIndicator_ShouldReturnNumberWithPlusIndicator() =>
        PhoneNumber.Parse("123456789").Map(number => number.NumberWithInternationalIndicator).Should()
            .BeSuccess("+123456789");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberContainsNonDigits() =>
        PhoneNumber.Parse("1234567abc123").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number can only contain digits."));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        PhoneNumber.Parse(value).Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsHigherThan7() =>
        PhoneNumber.Parse("123456").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be lower than 7."));

    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsLowerThan15() =>
        PhoneNumber.Parse("1234567890123456").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be higher than 15."));

    [Theory]
    [InlineData("1234567", "1234567")]
    [InlineData("123456789012345", "123456789012345")]
    public void Parse_ShouldReturnSuccess_GivenNumberIsValid(string value, string expected) =>
        PhoneNumber.Parse(value).Map(number => number.Number).Should().BeSuccess(expected);

    [Theory]
    [InlineData("+1234567890", "1234567890")]
    [InlineData("+123456789012345", "123456789012345")]
    [InlineData("+++1234567890", "1234567890")]
    public void Parse_ShouldReturnSuccess_GivenNumberStartWithPlus(string value, string expected) =>
        PhoneNumber.Parse(value).Map(number => number.Number).Should().BeSuccess(expected);

    [Fact]
    public void ToString_ShouldReturnNumber() =>
        PhoneNumber.Parse("123456789").Map(number => number.ToString()).Should().BeSuccess("123456789");
}