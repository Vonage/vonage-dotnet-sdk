using Vonage.Conversations.DeleteConversation;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.DeleteConversation
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            DeleteConversationRequest.Parse("CON-123")
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v1/conversations/CON-123");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenIdIsEmpty(string invalidId) =>
            DeleteConversationRequest.Parse(invalidId)
                .Should()
                .BeParsingFailure("ConversationId cannot be null or whitespace.");

        [Fact]
        public void Parse_ShouldSetConversationId() =>
            DeleteConversationRequest.Parse("CON-123")
                .Map(request => request.ConversationId)
                .Should()
                .BeSuccess("CON-123");
    }
}