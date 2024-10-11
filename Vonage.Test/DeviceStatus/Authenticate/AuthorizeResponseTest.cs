using FluentAssertions;
using Vonage.DeviceStatus.Authenticate;
using Xunit;

namespace Vonage.Test.DeviceStatus.Authenticate;

[Trait("Category", "Request")]
public class AuthorizeResponseTest
{
    [Fact]
    public void BuildGetTokenRequest() =>
        new AuthorizeResponse("123456", 0, 0)
            .BuildGetTokenRequest()
            .Should().Be(new GetTokenRequest("123456"));
}