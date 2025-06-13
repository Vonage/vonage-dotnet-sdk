#region
using Vonage.ProactiveConnect.Events.GetEvents;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.ProactiveConnect.Events.GetEvents;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsAscending() =>
        GetEventsRequest.Build()
            .WithPage(25)
            .WithPageSize(50)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v0.1/bulk/events?page=25&page_size=50&order=asc");

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsDescending() =>
        GetEventsRequest.Build()
            .WithPage(25)
            .WithPageSize(50)
            .OrderByDescending()
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                "/v0.1/bulk/events?page=25&page_size=50&order=desc");
}