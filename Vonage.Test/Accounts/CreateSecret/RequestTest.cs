using Vonage.Test.Common.Extensions;
using Xunit;
using CreateSecretRequest = Vonage.Accounts.CreateSecret.CreateSecretRequest;

namespace Vonage.Test.Accounts.CreateSecret;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("example-4PI-s3cret")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/abcd1234/secrets");
}
