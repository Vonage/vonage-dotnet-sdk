#region
using Vonage.Common.Failures;
using Vonage.NumberVerification.Verify;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.NumberVerification.Verify;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberContainsNonDigits() =>
        VerifyRequest.Parse("123456abc789")
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number can only contain digits."));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        VerifyRequest.Parse(value)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberLengthIsHigherThan7() =>
        VerifyRequest.Parse("123456")
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be lower than 7."));

    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberLengthIsLowerThan15() =>
        VerifyRequest.Parse("1234567890123456")
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be higher than 15."));

    [Theory]
    [InlineData("1234567", "1234567")]
    [InlineData("123456789012345", "123456789012345")]
    [InlineData("+1234567890", "1234567890")]
    [InlineData("+123456789012345", "123456789012345")]
    [InlineData("+++1234567890", "1234567890")]
    public void Build_ShouldSetPhoneNumber_GivenNumberIsValid(string value, string expected) =>
        VerifyRequest.Parse(value)
            .Map(number => number.PhoneNumber.Number)
            .Should()
            .BeSuccess(expected);

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        VerifyRequest.Parse("123456789")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("camara/number-verification/v031/verify");
}