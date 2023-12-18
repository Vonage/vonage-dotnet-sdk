using FluentAssertions;
using Vonage.Conversations.CreateConversation;
using Xunit;

namespace Vonage.Test.Unit.Conversations.CreateConversation
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            new CreateConversationRequest()
                .GetEndpointPath()
                .Should()
                .Be("/v1/conversations");
    }
}