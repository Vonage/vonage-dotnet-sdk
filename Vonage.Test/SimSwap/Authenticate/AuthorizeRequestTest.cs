using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.Authenticate;

[Trait("Category", "Request")]
public class AuthorizeRequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        Vonage.SimSwap.Authenticate.AuthenticateRequest.Parse("123456789")
            .Map(request => request.BuildAuthorizeRequest())
            .Map(r => r.GetEndpointPath())
            .Should().BeSuccess("oauth2/bc-authorize");
}