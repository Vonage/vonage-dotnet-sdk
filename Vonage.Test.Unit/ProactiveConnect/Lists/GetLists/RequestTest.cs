using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.GetLists;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.GetLists
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetListsRequest.Build()
                .WithPage(25)
                .WithPageSize(50)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/lists?page=25&page_size=50");
    }
}