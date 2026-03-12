#region
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetAddresses;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetAddresses;

[Trait("Category", "Request")]
[Trait("Product", "Voice")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetAddressesRequest.Build()
            .WithPage(Constants.ValidPage)
            .WithPageSize(Constants.ValidPageSize)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v1/addresses?page={Constants.ValidPage}&page_size={Constants.ValidPageSize}");
}