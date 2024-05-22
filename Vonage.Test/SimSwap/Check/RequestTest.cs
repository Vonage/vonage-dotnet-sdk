using Vonage.SimSwap.Check;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.Check;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CheckRequest.Build()
            .WithPhoneNumber("123456789")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("camara/sim-swap/v040/check");
}