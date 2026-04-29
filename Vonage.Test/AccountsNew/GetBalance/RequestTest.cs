using FluentAssertions;
using Vonage.AccountsNew.GetBalance;
using Xunit;

namespace Vonage.Test.AccountsNew.GetBalance;

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
