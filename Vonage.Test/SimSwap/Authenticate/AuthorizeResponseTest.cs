#region
using FluentAssertions;
using Vonage.SimSwap.Authenticate;
using Xunit;
#endregion

namespace Vonage.Test.SimSwap.Authenticate;

[Trait("Category", "Request")]
[Trait("Product", "SimSwap")]
public class AuthorizeResponseTest
{
    [Fact]
    public void BuildGetTokenRequest() =>
        new AuthorizeResponse("123456", 0, 0)
            .BuildGetTokenRequest()
            .Should().Be(new GetTokenRequest("123456"));
}