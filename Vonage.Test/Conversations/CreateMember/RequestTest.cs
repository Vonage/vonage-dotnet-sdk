#region
using Vonage.Conversations;
using Vonage.Conversations.CreateMember;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.CreateMember;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateMemberRequest.Build()
            .WithConversationId("CON-123")
            .WithState(CreateMemberRequest.AvailableStates.Invited)
            .WithUser(new MemberUser("USR-123", "USR-123"))
            .WithApp("USR-123", ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/conversations/CON-123/members");
}