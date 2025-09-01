#region
using AutoFixture;
using Vonage.SubAccounts.UpdateSubAccount;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.UpdateSubAccount;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Fixture fixture;
    public RequestTest() => this.fixture = new Fixture();

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey("8489Ef985")
            .WithName(this.fixture.Create<string>())
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/489dsSS564652/subaccounts/8489Ef985");

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey("8489Ef985")
            .WithName(this.fixture.Create<string>())
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts//subaccounts/8489Ef985");
}