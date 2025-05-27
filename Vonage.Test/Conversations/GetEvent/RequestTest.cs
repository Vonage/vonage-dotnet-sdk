#region
using Vonage.Conversations.GetEvent;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.GetEvent;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetEventRequest.Parse("CON-123", "EVE-123")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/conversations/CON-123/events/EVE-123");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenConversationIdIsEmpty(string invalidId) =>
        GetEventRequest.Parse(invalidId, "MEM-123")
            .Should()
            .BeParsingFailure("ConversationId cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenEventIdIsEmpty(string invalidId) =>
        GetEventRequest.Parse("CON-123", invalidId)
            .Should()
            .BeParsingFailure("EventId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldSetConversationId() =>
        GetEventRequest.Parse("CON-123", "EVE-123")
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess("CON-123");

    [Fact]
    public void Parse_ShouldSetMemberId() =>
        GetEventRequest.Parse("CON-123", "EVE-123")
            .Map(request => request.EventId)
            .Should()
            .BeSuccess("EVE-123");
}