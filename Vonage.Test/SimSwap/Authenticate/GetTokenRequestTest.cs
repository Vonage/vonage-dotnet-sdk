#region
using FluentAssertions;
using Vonage.SimSwap.Authenticate;
using Xunit;
#endregion

namespace Vonage.Test.SimSwap.Authenticate;

[Trait("Category", "Request")]
public class GetTokenRequestTest
{
    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        new GetTokenRequest("123456").BuildRequestMessage().RequestUri!.ToString().Should().Be("oauth2/token");
}