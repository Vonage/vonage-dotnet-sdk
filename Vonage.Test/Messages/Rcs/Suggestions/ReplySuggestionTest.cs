#region
using FluentAssertions;
using Vonage.Messages.Rcs.Suggestions;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs.Suggestions;

public class ReplySuggestionTest
{
    private const string Type = "reply";
    private const string Text = "Action 1";
    private const string PostbackData = "action_1";

    [Fact]
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void Type_ShouldBeReply() => BuildSuggestion().Type.Should().Be(Type);

    private static ReplySuggestion BuildSuggestion() => new ReplySuggestion(Text, PostbackData);
}