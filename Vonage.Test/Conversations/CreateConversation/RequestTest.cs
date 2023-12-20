using Vonage.Conversations.CreateConversation;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.CreateConversation
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateConversationRequest.Build()
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v1/conversations");
    }
}