using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.SimSwap.Check;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.DeviceStatus.GetConnectivityStatus;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetConnectivityStatusRequest.Build()
            .WithPhoneNumber("123456789")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("camara/device-status/v050/connectivity");
}