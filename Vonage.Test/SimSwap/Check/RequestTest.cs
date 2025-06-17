#region
using Vonage.SimSwap.Check;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.SimSwap.Check;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        CheckRequest.Build()
            .WithPhoneNumber("123456789")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("camara/sim-swap/v040/check");
}