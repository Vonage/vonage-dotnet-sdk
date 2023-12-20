using Vonage.ProactiveConnect.Lists.GetLists;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Lists.GetLists
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsAscending() =>
            GetListsRequest.Build()
                .WithPage(25)
                .WithPageSize(50)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/lists?page=25&page_size=50&order=asc");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsDescending() =>
            GetListsRequest.Build()
                .WithPage(25)
                .WithPageSize(50)
                .OrderByDescending()
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/lists?page=25&page_size=50&order=desc");
    }
}