using Vonage.AccountsNew.GetSecrets;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.GetSecrets;

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
