using Vonage.ProactiveConnect.Lists.CreateList;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.CreateList
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateListRequest.Build()
                .WithName("Random name")
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/lists");
    }
}