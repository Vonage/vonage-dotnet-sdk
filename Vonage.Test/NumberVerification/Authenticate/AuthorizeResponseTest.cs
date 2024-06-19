using FluentAssertions;
using Vonage.NumberVerification.Authenticate;
using Xunit;

namespace Vonage.Test.NumberVerification.Authenticate;

[Trait("Category", "Request")]
public class AuthorizeResponseTest
{
    [Fact]
    public void BuildGetTokenRequest() =>
        new AuthorizeResponse("123456", 0, 0)
            .BuildGetTokenRequest()
            .Should().Be(new GetTokenRequest("123456"));
}