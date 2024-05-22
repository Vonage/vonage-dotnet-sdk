using Vonage.Common.Failures;
using Vonage.SimSwap.GetSwapDate;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.GetSwapDate;

[Trait("Category", "Request")]
public class RequestTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        GetSwapDateRequest.Parse(value).Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberContainsNonDigits() =>
        GetSwapDateRequest.Parse("1234567abc123").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number can only contain digits."));
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsHigherThan7() =>
        GetSwapDateRequest.Parse("123456").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be lower than 7."));
    
    [Fact]
    public void Parse_ShouldReturnFailure_GivenNumberLengthIsLowerThan15() =>
        GetSwapDateRequest.Parse("1234567890123456").Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be higher than 15."));
    
    [Theory]
    [InlineData("1234567", "1234567")]
    [InlineData("123456789012345", "123456789012345")]
    [InlineData("+1234567890", "1234567890")]
    [InlineData("+123456789012345", "123456789012345")]
    [InlineData("+++1234567890", "1234567890")]
    public void Parse_ShouldReturnSuccess(string value, string expected) =>
        GetSwapDateRequest.Parse(value)
            .Map(request => request.PhoneNumber.Number)
            .Should()
            .BeSuccess(expected);
    
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetSwapDateRequest.Parse("123456789")
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("camara/sim-swap/v040/retrieve-date");
}