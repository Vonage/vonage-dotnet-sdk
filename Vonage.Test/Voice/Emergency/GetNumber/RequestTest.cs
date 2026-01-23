#region
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetNumber;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetNumber;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void Parse_ShouldReturnFailure_GivenPhoneNumberIsIncorrect() =>
        GetNumberRequest.Parse(Constants.InvalidNumber)
            .Should()
            .BeResultFailure("Number can only contain digits.");

    [Fact]
    public void Parse_ShouldReturnSuccess_WhenNumberIsInCorrectFormat() =>
        GetNumberRequest.Parse(Constants.ValidNumber)
            .Should()
            .BeSuccess();

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetNumberRequest.Parse(Constants.ValidNumber)
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/emergency/numbers/33601020304");
}