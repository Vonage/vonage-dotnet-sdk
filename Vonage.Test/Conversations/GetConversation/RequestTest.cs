#region
using Vonage.Conversations.GetConversation;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.GetConversation;

[Trait("Category", "Request")]
public class RequestTest
{
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

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetConversationRequest.Parse("CON-123")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/conversations/CON-123");
}