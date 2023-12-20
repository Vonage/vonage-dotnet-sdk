using System;
using System.Globalization;
using Vonage.Common.Monads;
using Vonage.Conversations.GetConversations;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetConversations
{
    public class RequestBuilderTest
    {
        [Fact]
        public void Build_ShouldHaveDefaultOrder() =>
            GetConversationsRequest
                .Build()
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Ascending);

        [Fact]
        public void Build_ShouldHaveDefaultPageSize() =>
            GetConversationsRequest
                .Build()
                .Create()
                .Map(request => request.PageSize)
                .Should()
                .BeSuccess(10);

        [Fact]
        public void Build_ShouldHaveNoDefaultCursor() =>
            GetConversationsRequest
                .Build()
                .Create()
                .Map(request => request.Cursor)
                .Should()
                .BeSuccess(Maybe<string>.None);

        [Fact]
        public void Build_ShouldHaveNoDefaultEndDate() =>
            GetConversationsRequest
                .Build()
                .Create()
                .Map(request => request.EndDate)
                .Should()
                .BeSuccess(Maybe<DateTimeOffset>.None);

        [Fact]
        public void Build_ShouldHaveNoDefaultStartDate() =>
            GetConversationsRequest
                .Build()
                .Create()
                .Map(request => request.StartDate)
                .Should()
                .BeSuccess(Maybe<DateTimeOffset>.None);

        [Fact]
        public void Build_ShouldReturnFailure_GivenPageSizeIsHigherThanOneHundred() =>
            GetConversationsRequest
                .Build()
                .WithPageSize(101)
                .Create()
                .Should()
                .BeParsingFailure("PageSize cannot be higher than 100.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenPageSizeIsLowerThanOne() =>
            GetConversationsRequest
                .Build()
                .WithPageSize(0)
                .Create()
                .Should()
                .BeParsingFailure("PageSize cannot be lower than 1.");

        [Fact]
        public void Build_ShouldSetEndDate() =>
            GetConversationsRequest
                .Build()
                .WithEndDate(DateTimeOffset.Parse("2018-01-01 10:00:00", CultureInfo.InvariantCulture))
                .Create()
                .Map(request => request.EndDate)
                .Should()
                .BeSuccess(DateTimeOffset.Parse("2018-01-01 10:00:00", CultureInfo.InvariantCulture));

        [Fact]
        public void Build_ShouldSetOrder() =>
            GetConversationsRequest
                .Build()
                .WithOrder(FetchOrder.Descending)
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Descending);

        [Fact]
        public void Build_ShouldSetPageSize() =>
            GetConversationsRequest
                .Build()
                .WithPageSize(50)
                .Create()
                .Map(request => request.PageSize)
                .Should()
                .BeSuccess(50);

        [Fact]
        public void Build_ShouldSetStartDate() =>
            GetConversationsRequest
                .Build()
                .WithStartDate(DateTimeOffset.Parse("2018-01-01 10:00:00", CultureInfo.InvariantCulture))
                .Create()
                .Map(request => request.StartDate)
                .Should()
                .BeSuccess(DateTimeOffset.Parse("2018-01-01 10:00:00", CultureInfo.InvariantCulture));
    }
}