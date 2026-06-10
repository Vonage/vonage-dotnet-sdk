using Vonage.Test.Common.Extensions;
using Xunit;
using ChangeAccountSettingsRequest = Vonage.Accounts.ChangeAccountSettings.ChangeAccountSettingsRequest;

namespace Vonage.Test.Accounts.ChangeAccountSettings;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        ChangeAccountSettingsRequest.Build()
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/account/settings");
}
