using FluentAssertions;
using Vonage.NumberVerification.Authenticate;
using Xunit;

namespace Vonage.Test.NumberVerification.Authenticate;

[Trait("Category", "Request")]
public class GetTokenRequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        new GetTokenRequest("123456").GetEndpointPath().Should().Be("oauth2/token");
}