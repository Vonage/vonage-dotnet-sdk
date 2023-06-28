using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.UpdateSubAccount;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.UpdateSubAccount
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            UpdateSubAccountRequest
                .Build()
                .WithSubAccountKey("8489Ef985")
                .WithName(this.fixture.Create<string>())
                .Create()
                .Map(request => request.WithApiKey("489dsSS564652"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts/489dsSS564652/subaccounts/8489Ef985");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
            UpdateSubAccountRequest
                .Build()
                .WithSubAccountKey("8489Ef985")
                .WithName(this.fixture.Create<string>())
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts//subaccounts/8489Ef985");
    }
}