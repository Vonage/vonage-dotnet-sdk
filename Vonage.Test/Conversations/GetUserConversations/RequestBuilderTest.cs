using System;
using System.Globalization;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.GetUserConversations;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetUserConversations;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenUserIdIsEmpty(string invalidId) =>
        GetUserConversationsRequest.Build()
            .WithUserId(invalidId)
            .Create()
            .Should()
            .BeParsingFailure("UserId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetUserId() =>
        GetUserConversationsRequest.Build()
            .WithUserId("US-123")
            .Create()
            .Map(request => request.UserId)
            .Should()
            .BeSuccess("US-123");

    [Fact]
    public void Build_ShouldHaveDefaultOrder() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .Create()
            .Map(request => request.Order)
            .Should()
            .BeSuccess(FetchOrder.Ascending);

    [Fact]
    public void Build_ShouldHaveDefaultPageSize() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(10);

    [Fact]
    public void Build_ShouldHaveNoDefaultCursor() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .Create()
            .Map(request => request.Cursor)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldHaveNoDefaultStartDate() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .Create()
            .Map(request => request.StartDate)
            .Should()
            .BeSuccess(Maybe<DateTimeOffset>.None);

    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsHigherThanOneHundred() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .WithPageSize(101)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be higher than 100.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsLowerThanOne() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .WithPageSize(0)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be lower than 1.");

    [Fact]
    public void Build_ShouldSetOrder() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .WithOrder(FetchOrder.Descending)
            .Create()
            .Map(request => request.Order)
            .Should()
            .BeSuccess(FetchOrder.Descending);

    [Fact]
    public void Build_ShouldSetPageSize() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .WithPageSize(50)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(50);

    [Fact]
    public void Build_ShouldSetStartDate() =>
        GetUserConversationsRequest
            .Build()
            .WithUserId("US-123")
            .WithStartDate(DateTimeOffset.Parse("2018-01-01 10:00:00", CultureInfo.InvariantCulture))
            .Create()
            .Map(request => request.StartDate)
            .Should()
            .BeSuccess(DateTimeOffset.Parse("2018-01-01 10:00:00", CultureInfo.InvariantCulture));
}