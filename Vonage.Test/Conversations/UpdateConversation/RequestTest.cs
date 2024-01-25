using Vonage.Conversations.UpdateConversation;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.UpdateConversation;

public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        UpdateConversationRequest.Build()
            .WithConversationId("CON-1234")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v1/conversations/CON-1234");
}