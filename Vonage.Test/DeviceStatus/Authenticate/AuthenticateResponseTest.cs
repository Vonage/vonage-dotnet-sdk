using System.Net.Http.Headers;
using FluentAssertions;
using Vonage.DeviceStatus.Authenticate;
using Xunit;

namespace Vonage.Test.DeviceStatus.Authenticate;

[Trait("Category", "Request")]
public class AuthenticateResponseTest
{
    [Fact]
    public void BuildAuthenticationHeader_ShouldReturnBearerAuth() =>
        new AuthenticateResponse("123456789")
            .BuildAuthenticationHeader()
            .Should()
            .Be(new AuthenticationHeaderValue("Bearer", "123456789"));
}