#region
using Vonage.Conversations.DeleteConversation;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.DeleteConversation;

[Trait("Category", "Request")]
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

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        DeleteConversationRequest.Parse("CON-123")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/conversations/CON-123");
}