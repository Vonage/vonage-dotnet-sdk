using Vonage.AccountsNew.TopUpBalance;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.TopUpBalance;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        TopUpBalanceRequest.Build()
            .WithTransactionReference("8ef2447e69604f642ae59363aa5f781b")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/account/top-up");
}
