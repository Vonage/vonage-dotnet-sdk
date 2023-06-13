using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.GetSubAccount;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetSubAccount
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetSubAccountRequest.Parse("456iFuDL099")
                .Map(request => request.WithApiKey("123abCD"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts/123abCD/subaccounts/456iFuDL099");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
            GetSubAccountRequest.Parse("456iFuDL099")
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts//subaccounts/456iFuDL099");
    }
}