using System;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.GetItems;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.GetItems
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsAscending() =>
            GetItemsRequest.Build()
                .WithListId(new Guid("e250d4ae-eb43-4eee-a901-88ce4420aed3"))
                .WithPage(25)
                .WithPageSize(50)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    "/v0.1/bulk/lists/e250d4ae-eb43-4eee-a901-88ce4420aed3/items?page=25&page_size=50&order=asc");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_GivenOrderIsDescending() =>
            GetItemsRequest.Build()
                .WithListId(new Guid("e250d4ae-eb43-4eee-a901-88ce4420aed3"))
                .WithPage(25)
                .WithPageSize(50)
                .OrderByDescending()
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    "/v0.1/bulk/lists/e250d4ae-eb43-4eee-a901-88ce4420aed3/items?page=25&page_size=50&order=desc");
    }
}