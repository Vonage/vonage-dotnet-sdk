#region
using FluentAssertions;
using Vonage.SubAccounts.GetSubAccounts;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.GetSubAccounts;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetSubAccountsRequest.Build("123abCD")
            .BuildRequestMessage()
            .RequestUri!.ToString().Should()
            .Be("/accounts/123abCD/subaccounts");
}