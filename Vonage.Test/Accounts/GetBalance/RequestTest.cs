using FluentAssertions;
using Vonage.Accounts.GetBalance;
using Xunit;

namespace Vonage.Test.Accounts.GetBalance;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetBalanceRequest.Default
            .BuildRequestMessage()
            .RequestUri!.ToString()
            .Should().Be("/account/get-balance");
}
