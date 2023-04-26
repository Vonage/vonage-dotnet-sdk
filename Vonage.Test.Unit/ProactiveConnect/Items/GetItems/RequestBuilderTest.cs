using System;
using AutoFixture;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.GetItems;
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
                .BeFailure(ResultFailure.FromErrorMessage("ListId cannot be empty."));

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