using Vonage.Common.Monads;
using Vonage.Conversations.UpdateMember;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.UpdateMember;

public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetConversationId() =>
        SerializationTest.BuildRequestWithJoinedState()
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess(SerializationTest.ValidConversationId);
    
    [Fact]
    public void Build_ShouldSetMemberId() =>
        SerializationTest.BuildRequestWithJoinedState()
            .Map(request => request.MemberId)
            .Should()
            .BeSuccess(SerializationTest.ValidMemberId);
    
    [Fact]
    public void Build_ShouldSetJoinedState_GivenWithJoinedState() =>
        SerializationTest.BuildRequestWithJoinedState()
            .Map(request => request.State)
            .Should()
            .BeSuccess(UpdateMemberRequest.AvailableStates.Joined);
    
    [Fact]
    public void Build_ShouldSetLeftState_GivenWithLeftState() =>
        SerializationTest.BuildRequestWithLeftState()
            .Map(request => request.State)
            .Should()
            .BeSuccess(UpdateMemberRequest.AvailableStates.Left);
    
    [Fact]
    public void Build_ShouldHaveNoReason_GivenWithJoinedState() =>
        SerializationTest.BuildRequestWithJoinedState()
            .Map(request => request.Reason)
            .Should()
            .BeSuccess(Maybe<Reason>.None);
    
    [Fact]
    public void Build_ShouldSetReason_GivenWithLeftState() =>
        SerializationTest.BuildRequestWithLeftState()
            .Map(request => request.Reason)
            .Should()
            .BeSuccess(SerializationTest.ValidReason);
    
    [Fact]
    public void Build_ShouldSetFrom() =>
        SerializationTest.BuildRequestWithFrom()
            .Map(request => request.From)
            .Should()
            .BeSuccess(SerializationTest.ValidFrom);
    
    [Fact]
    public void Build_ShouldHaveNoFrom_GivenDefault() =>
        SerializationTest.BuildRequestWithJoinedState()
            .Map(request => request.From)
            .Should()
            .BeSuccess(Maybe<string>.None);
}