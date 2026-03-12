#region
using FluentAssertions;
using Vonage.Common.Failures;
using Xunit;
#endregion

namespace Vonage.Test.Common.Failures;

[Trait("Category", "Core")]
[Trait("Product", "Common")]
public class AuthenticationFailureTest
{
    [Fact]
    public void Type_ShouldReturnAuthenticationFailure() => new AuthenticationFailure()
        .Type
        .Should()
        .Be(typeof(AuthenticationFailure));
}