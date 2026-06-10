using Vonage.Test.Common.Extensions;
using Xunit;
using GetSecretsRequest = Vonage.Accounts.GetSecrets.GetSecretsRequest;

namespace Vonage.Test.Accounts.GetSecrets;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetApiKey() =>
        GetSecretsRequest.Build()
            .WithApiKey("abcd1234")
            .Create()
            .Map(r => r.ApiKey)
            .Should()
            .BeSuccess("abcd1234");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenApiKeyIsNullOrWhitespace(string invalidKey) =>
        GetSecretsRequest.Build()
            .WithApiKey(invalidKey)
            .Create()
            .Should()
            .BeParsingFailure("ApiKey cannot be null or whitespace.");
}
