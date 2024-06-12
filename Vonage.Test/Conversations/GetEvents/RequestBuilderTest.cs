using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.GetEvents;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetEvents;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidConversationId = "CON-123";

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenConversationIdIsEmpty(string invalidId) =>
        GetEventsRequest
            .Build()
            .WithConversationId(invalidId)
            .Create()
            .Should()
            .BeParsingFailure("ConversationId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetConversationId() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess(ValidConversationId);

    [Fact]
    public void Build_ShouldHaveDefaultOrder() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.Order)
            .Should()
            .BeSuccess(FetchOrder.Ascending);

    [Fact]
    public void Build_ShouldHaveDefaultPageSize() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(10);

    [Fact]
    public void Build_ShouldHaveNoDefaultCursor() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.Cursor)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldHaveNoDefaultEventType() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.EventType)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldHaveNoDefaultEndId() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.EndId)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldHaveNoDefaultStartId() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.StartId)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsHigherThanOneHundred() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithPageSize(101)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be higher than 100.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsLowerThanOne() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithPageSize(0)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be lower than 1.");

    [Fact]
    public void Build_ShouldSetEndId() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithEndId("123")
            .Create()
            .Map(request => request.EndId)
            .Should()
            .BeSuccess("123");

    [Fact]
    public void Build_ShouldSetOrder() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithOrder(FetchOrder.Descending)
            .Create()
            .Map(request => request.Order)
            .Should()
            .BeSuccess(FetchOrder.Descending);

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void Build_ShouldSetPageSize(int pageSize) =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithPageSize(pageSize)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(pageSize);

    [Fact]
    public void Build_ShouldSetStartId() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithStartId("123")
            .Create()
            .Map(request => request.StartId)
            .Should()
            .BeSuccess("123");

    [Fact]
    public void Build_ShouldSetEventType() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithEventType("type")
            .Create()
            .Map(request => request.EventType)
            .Should()
            .BeSuccess("type");

    [Fact]
    public void Build_ShouldIncludeDeletedEventsGivenDefault() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .Create()
            .Map(request => request.ExcludeDeletedEvents)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldExcludeDeletedEvents() =>
        GetEventsRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .ExcludeDeletedEvents()
            .Create()
            .Map(request => request.ExcludeDeletedEvents)
            .Should()
            .BeSuccess(true);
}