using System;
using AutoFixture;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.GetItem;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.GetItem
{
    public class RequestBuilderTest
    {
        private readonly Guid listId;
        private readonly Guid itemId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.listId = fixture.Create<Guid>();
            this.itemId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenDataIsEmpty() =>
            GetItemRequest
                .Build()
                .WithListId(this.listId)
                .WithItemId(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ItemId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenListIdIsEmpty() =>
            GetItemRequest
                .Build()
                .WithListId(Guid.Empty)
                .WithItemId(this.itemId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ListId cannot be empty."));

        [Fact]
        public void Build_ShouldSetData() =>
            GetItemRequest
                .Build()
                .WithListId(this.listId)
                .WithItemId(this.itemId)
                .Create()
                .Map(request => request.ItemId)
                .Should()
                .BeSuccess(this.itemId);

        [Fact]
        public void Build_ShouldSetListId() =>
            GetItemRequest
                .Build()
                .WithListId(this.listId)
                .WithItemId(this.itemId)
                .Create()
                .Map(request => request.ListId)
                .Should()
                .BeSuccess(this.listId);
    }
}