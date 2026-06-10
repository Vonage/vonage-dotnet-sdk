using Vonage.Test.Common.Extensions;
using Xunit;
using TopUpBalanceRequest = Vonage.Accounts.TopUpBalance.TopUpBalanceRequest;

namespace Vonage.Test.Accounts.TopUpBalance;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetTransactionReference() =>
        TopUpBalanceRequest.Build()
            .WithTransactionReference("8ef2447e69604f642ae59363aa5f781b")
            .Create()
            .Map(r => r.TransactionReference)
            .Should()
            .BeSuccess("8ef2447e69604f642ae59363aa5f781b");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenTransactionReferenceIsNullOrWhitespace(string invalidTrx) =>
        TopUpBalanceRequest.Build()
            .WithTransactionReference(invalidTrx)
            .Create()
            .Should()
            .BeParsingFailure("TransactionReference cannot be null or whitespace.");
}
