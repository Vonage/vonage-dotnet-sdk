using Vonage.Conversations.UpdateMember;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.UpdateMember;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        UpdateMemberRequest.Build()
            .WithConversationId("CON-123")
            .WithMemberId("MEM-123")
            .WithJoinedState()
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v1/conversations/CON-123/members/MEM-123");
}