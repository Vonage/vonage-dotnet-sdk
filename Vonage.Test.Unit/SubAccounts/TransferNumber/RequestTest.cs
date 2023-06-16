using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.SubAccounts.TransferNumber;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.TransferNumber
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            TransferNumberRequest
                .Build()
                .WithFrom(this.fixture.Create<string>())
                .WithTo(this.fixture.Create<string>())
                .WithNumber(this.fixture.Create<string>())
                .WithCountry(StringHelper.GenerateString(2))
                .Create()
                .Map(request => request.WithApiKey("489dsSS564652"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts/489dsSS564652/transfer-number/");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
            TransferNumberRequest
                .Build()
                .WithFrom(this.fixture.Create<string>())
                .WithTo(this.fixture.Create<string>())
                .WithNumber(this.fixture.Create<string>())
                .WithCountry(StringHelper.GenerateString(2))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts//transfer-number/");
    }
}