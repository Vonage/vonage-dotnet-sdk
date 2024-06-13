using System.Text.Json;
using Vonage.Common.Monads;
using Vonage.Conversations.CreateEvent;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.CreateEvent;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidConversationId = "CON-123";
    private const string ValidType = "submitted";

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenConversationIdIsEmpty(string invalidId) =>
        CreateEventRequest
            .Build()
            .WithConversationId(invalidId)
            .WithType(ValidType)
            .WithBody(BuildValidBody())
            .Create()
            .Should()
            .BeParsingFailure("ConversationId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetConversationId() =>
        CreateEventRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithType(ValidType)
            .WithBody(BuildValidBody())
            .Create()
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess(ValidConversationId);

    [Fact]
    public void Build_ShouldSetType() =>
        CreateEventRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithType(ValidType)
            .WithBody(BuildValidBody())
            .Create()
            .Map(request => request.Type)
            .Should()
            .BeSuccess(ValidType);

    [Fact]
    public void Build_ShouldHaveNoFrom_GivenDefault() =>
        CreateEventRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithType(ValidType)
            .WithBody(BuildValidBody())
            .Create()
            .Map(request => request.From)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldSetFrom() =>
        CreateEventRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithType(ValidType)
            .WithBody(BuildValidBody())
            .WithFrom("from")
            .Create()
            .Map(request => request.From)
            .Should()
            .BeSuccess("from");

    [Fact]
    public void Build_ShouldSetBody() =>
        CreateEventRequest
            .Build()
            .WithConversationId(ValidConversationId)
            .WithType(ValidType)
            .WithBody(BuildValidBody())
            .Create()
            .Map(request => request.Body)
            .Should()
            .BeSuccess(body => body.Should().Be(BuildValidBody()));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenTypeIsEmpty(string invalidType) =>
        CreateEventRequest
            .Build()
            .WithConversationId(ValidType)
            .WithType(invalidType)
            .WithBody(BuildValidBody())
            .Create()
            .Should()
            .BeParsingFailure("Type cannot be null or whitespace.");

    private static JsonElement BuildValidBody() =>
        JsonSerializer.SerializeToElement(new {type = "action", value = "random"});
}