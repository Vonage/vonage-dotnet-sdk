using Vonage.Common.Test.Extensions;
using Vonage.Conversations.DeleteConversation;
using Xunit;

namespace Vonage.Test.Unit.Conversations.DeleteConversation
{
    public class RequestTest
    {
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