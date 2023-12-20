using AutoFixture;
using Vonage.ProactiveConnect;
using Vonage.ProactiveConnect.Events.GetEvents;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Events.GetEvents
{
    public class RequestBuilderTest
    {
        private readonly int page;
        private readonly int pageSize;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.page = fixture.Create<int>();
            this.pageSize = fixture.Create<int>();
        }

        [Fact]
        public void Build_ShouldReturnSuccess() =>
            GetEventsRequest
                .Build()
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Map(request => request.Page)
                .Should()
                .BeSuccess(this.page);

        [Fact]
        public void Build_ShouldSetOrderAscending_GivenDefault() =>
            GetEventsRequest
                .Build()
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Ascending);

        [Fact]
        public void Build_ShouldSetOrderDescending_GivenOrderByDescendingIsUsed() =>
            GetEventsRequest
                .Build()
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .OrderByDescending()
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Descending);

        [Fact]
        public void Build_ShouldSetPageSize() =>
            GetEventsRequest
                .Build()
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Map(request => request.PageSize)
                .Should()
                .BeSuccess(this.pageSize);
    }
}