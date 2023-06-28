using FluentAssertions;
using Vonage.SubAccounts.GetSubAccounts;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetSubAccounts
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetSubAccountsRequest.Build("123abCD")
                .GetEndpointPath()
                .Should()
                .Be("/accounts/123abCD/subaccounts");
    }
}