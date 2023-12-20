using System;
using Vonage.ProactiveConnect.Lists.UpdateList;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.UpdateList
{
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
}