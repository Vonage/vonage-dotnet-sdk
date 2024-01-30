using System;
using Vonage.ProactiveConnect.Lists.UpdateList;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Lists.UpdateList;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        UpdateListRequest.Build()
            .WithListId(new Guid("8ef94367-3a18-47a7-b59e-e98835194dcb"))
            .WithName("Random name")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v0.1/bulk/lists/8ef94367-3a18-47a7-b59e-e98835194dcb");
}