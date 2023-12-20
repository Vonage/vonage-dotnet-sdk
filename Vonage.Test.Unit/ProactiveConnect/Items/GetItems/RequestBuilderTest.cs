using System;
using AutoFixture;
using Vonage.ProactiveConnect;
using Vonage.ProactiveConnect.Items.GetItems;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.GetItems
{
    public class RequestBuilderTest
    {
        private readonly Guid listId;
        private readonly int page;
        private readonly int pageSize;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.listId = fixture.Create<Guid>();
            this.page = fixture.Create<int>();
            this.pageSize = fixture.Create<int>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenListIdIsEmpty() =>
            GetItemsRequest
                .Build()
                .WithListId(Guid.Empty)
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Should()
                .BeParsingFailure("ListId cannot be empty.");

        [Fact]
        public void Build_ShouldSetListId() =>
            GetItemsRequest
                .Build()
                .WithListId(this.listId)
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Map(request => request.ListId)
                .Should()
                .BeSuccess(this.listId);

        [Fact]
        public void Build_ShouldSetOrderAscending_GivenDefault() =>
            GetItemsRequest
                .Build()
                .WithListId(this.listId)
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Ascending);

        [Fact]
        public void Build_ShouldSetOrderDescending_GivenOrderByDescendingIsUsed() =>
            GetItemsRequest
                .Build()
                .WithListId(this.listId)
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .OrderByDescending()
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Descending);

        [Fact]
        public void Build_ShouldSetPage() =>
            GetItemsRequest
                .Build()
                .WithListId(this.listId)
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Map(request => request.Page)
                .Should()
                .BeSuccess(this.page);

        [Fact]
        public void Build_ShouldSetPageSize() =>
            GetItemsRequest
                .Build()
                .WithListId(this.listId)
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Map(request => request.PageSize)
                .Should()
                .BeSuccess(this.pageSize);
    }
}