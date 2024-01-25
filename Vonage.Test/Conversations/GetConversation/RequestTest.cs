using Vonage.Conversations.GetConversation;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetConversation;

public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetConversationRequest.Parse("CON-123")
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v1/conversations/CON-123");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenIdIsEmpty(string invalidId) =>
        GetConversationRequest.Parse(invalidId)
            .Should()
            .BeParsingFailure("ConversationId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldSetConversationId() =>
        GetConversationRequest.Parse("CON-123")
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess("CON-123");
}