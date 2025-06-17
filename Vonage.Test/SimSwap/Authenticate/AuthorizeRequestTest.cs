#region
using Vonage.SimSwap.Authenticate;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.SimSwap.Authenticate;

[Trait("Category", "Request")]
public class AuthorizeRequestTest
{
    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        AuthenticateRequest.Parse("123456789", "scope")
            .Map(request => request.BuildAuthorizeRequest())
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should().BeSuccess("oauth2/bc-authorize");
}