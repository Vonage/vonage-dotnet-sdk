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
        GetNumberRequest.Parse("aa601020304")
            .Should()
            .BeResultFailure("Number can only contain digits.");

    [Fact]
    public void Parse_ShouldReturnSuccess_WhenNumberIsInCorrectFormat() =>
        GetNumberRequest.Parse("+33601020304")
            .Should()
            .BeSuccess();

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetNumberRequest.Parse("+33601020304")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/emergency/numbers/33601020304");
}