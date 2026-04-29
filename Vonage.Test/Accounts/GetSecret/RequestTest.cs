using Vonage.Test.Common.Extensions;
using Xunit;
using GetSecretRequest = Vonage.Accounts.GetSecret.GetSecretRequest;

namespace Vonage.Test.Accounts.GetSecret;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/abcd1234/secrets/ad6dc56f-07b5-46e1-a527-85530e625800");
}
