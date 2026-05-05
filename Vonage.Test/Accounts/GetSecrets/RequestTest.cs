using Vonage.Test.Common.Extensions;
using Xunit;
using GetSecretsRequest = Vonage.Accounts.GetSecrets.GetSecretsRequest;

namespace Vonage.Test.Accounts.GetSecrets;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetSecretsRequest.Build()
            .WithApiKey("abcd1234")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/abcd1234/secrets");
}
