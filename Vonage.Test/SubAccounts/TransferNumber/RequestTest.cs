#region
using AutoFixture;
using Vonage.SubAccounts.TransferNumber;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.TransferNumber;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Fixture fixture;
    public RequestTest() => this.fixture = new Fixture();

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.fixture.Create<string>())
            .WithTo(this.fixture.Create<string>())
            .WithNumber(this.fixture.Create<string>())
            .WithCountry(StringHelper.GenerateString(2))
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/489dsSS564652/transfer-number");

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.fixture.Create<string>())
            .WithTo(this.fixture.Create<string>())
            .WithNumber(this.fixture.Create<string>())
            .WithCountry(StringHelper.GenerateString(2))
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts//transfer-number");
}