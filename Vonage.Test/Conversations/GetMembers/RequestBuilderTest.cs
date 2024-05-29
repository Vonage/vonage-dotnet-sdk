using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.GetMembers;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetMembers;

public class RequestBuilderTest
{
    private const string ConversationId = "CON-123";
    
    [Fact]
    public void Build_ShouldHaveDefaultOrder() =>
        GetMembersRequest
            .Build()
            .WithConversationId(ConversationId)
            .Create()
            .Map(request => request.Order)
            .Should()
            .BeSuccess(FetchOrder.Ascending);
    
    [Fact]
    public void Build_ShouldHaveDefaultPageSize() =>
        GetMembersRequest
            .Build()
            .WithConversationId(ConversationId)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(10);
    
    [Fact]
    public void Build_ShouldHaveNoDefaultCursor() =>
        GetMembersRequest
            .Build()
            .WithConversationId(ConversationId)
            .Create()
            .Map(request => request.Cursor)
            .Should()
            .BeSuccess(Maybe<string>.None);
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsHigherThanOneHundred() =>
        GetMembersRequest
            .Build()
            .WithConversationId(ConversationId)
            .WithPageSize(101)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be higher than 100.");
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsLowerThanOne() =>
        GetMembersRequest
            .Build()
            .WithConversationId(ConversationId)
            .WithPageSize(0)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be lower than 1.");
    
    [Fact]
    public void Build_ShouldSetOrder() =>
        GetMembersRequest
            .Build()
            .WithConversationId(ConversationId)
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
        GetMembersRequest
            .Build()
            .WithConversationId(ConversationId)
            .WithPageSize(pageSize)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(pageSize);
}