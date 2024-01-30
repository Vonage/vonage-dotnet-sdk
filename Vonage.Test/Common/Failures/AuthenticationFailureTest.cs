using FluentAssertions;
using Vonage.Common.Failures;
using Xunit;

namespace Vonage.Test.Common.Failures;

[Trait("Category", "Unit")]
public class AuthenticationFailureTest
{
    [Fact]
    public void Type_ShouldReturnAuthenticationFailure() => new AuthenticationFailure()
        .Type
        .Should()
        .Be(typeof(AuthenticationFailure));
}