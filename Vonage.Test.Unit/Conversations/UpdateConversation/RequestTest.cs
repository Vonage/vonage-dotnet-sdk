using Vonage.Common.Test.Extensions;
using Vonage.Conversations.UpdateConversation;
using Xunit;

namespace Vonage.Test.Unit.Conversations.UpdateConversation
{
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
}