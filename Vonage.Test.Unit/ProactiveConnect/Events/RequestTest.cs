using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Events.GetEvents;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Events
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsAscending() =>
            GetEventsRequest.Build()
                .WithPage(25)
                .WithPageSize(50)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/events?page=25&page_size=50&order=asc");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsDescending() =>
            GetEventsRequest.Build()
                .WithPage(25)
                .WithPageSize(50)
                .OrderByDescending()
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    "/v0.1/bulk/events?page=25&page_size=50&order=desc");
    }
}