#region
using Vonage.Conversations.GetMember;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.GetMember;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetMemberRequest.Parse("CON-123", "MEM-123")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/conversations/CON-123/members/MEM-123");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenConversationIdIsEmpty(string invalidId) =>
        GetMemberRequest.Parse(invalidId, "MEM-123")
            .Should()
            .BeParsingFailure("ConversationId cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenMemberIdIsEmpty(string invalidId) =>
        GetMemberRequest.Parse("CON-123", invalidId)
            .Should()
            .BeParsingFailure("MemberId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldSetConversationId() =>
        GetMemberRequest.Parse("CON-123", "MEM-123")
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess("CON-123");

    [Fact]
    public void Parse_ShouldSetMemberId() =>
        GetMemberRequest.Parse("CON-123", "MEM-123")
            .Map(request => request.MemberId)
            .Should()
            .BeSuccess("MEM-123");
}