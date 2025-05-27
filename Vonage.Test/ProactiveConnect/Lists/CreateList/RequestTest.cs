#region
using Vonage.ProactiveConnect.Lists.CreateList;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.ProactiveConnect.Lists.CreateList;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateListRequest.Build()
            .WithName("Random name")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v0.1/bulk/lists");
}