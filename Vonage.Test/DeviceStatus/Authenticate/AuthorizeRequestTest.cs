using Vonage.DeviceStatus.Authenticate;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.DeviceStatus.Authenticate;

[Trait("Category", "Request")]
public class AuthorizeRequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        AuthenticateRequest.Parse("123456789", "scope")
            .Map(request => request.BuildAuthorizeRequest())
            .Map(r => r.GetEndpointPath())
            .Should().BeSuccess("oauth2/bc-authorize");
}