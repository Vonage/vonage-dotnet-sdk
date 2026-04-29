using Vonage.Test.Common.Extensions;
using Xunit;
using RevokeSecretRequest = Vonage.Accounts.RevokeSecret.RevokeSecretRequest;

namespace Vonage.Test.Accounts.RevokeSecret;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        RevokeSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/abcd1234/secrets/ad6dc56f-07b5-46e1-a527-85530e625800");
}
