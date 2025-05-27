#region
using Vonage.Common.Monads;
using Vonage.Conversations.DeleteEvent;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.DeleteEvent;

[Trait("Category", "Request")]
public class RequestTest
{
    private const string ValidConversationId = "CON-123";
    private const string ValidEventId = "EVE-123";

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        BuildRequest()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/conversations/CON-123/events/EVE-123");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenConversationIdIsEmpty(string invalidId) =>
        DeleteEventRequest.Parse(invalidId, ValidEventId)
            .Should()
            .BeParsingFailure("ConversationId cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenEventIdIsEmpty(string invalidId) =>
        DeleteEventRequest.Parse(ValidConversationId, invalidId)
            .Should()
            .BeParsingFailure("EventId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldSetConversationId() =>
        BuildRequest()
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess("CON-123");

    [Fact]
    public void Parse_ShouldSetEventId() =>
        BuildRequest()
            .Map(request => request.EventId)
            .Should()
            .BeSuccess("EVE-123");

    internal static Result<DeleteEventRequest> BuildRequest() =>
        DeleteEventRequest.Parse(ValidConversationId, ValidEventId);
}