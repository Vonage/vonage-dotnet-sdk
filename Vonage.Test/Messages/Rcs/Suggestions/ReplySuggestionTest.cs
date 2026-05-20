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

    [Fact]
    public void GetErrors_ReturnsEmpty_WhenValid() => BuildSuggestion().GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenTextIsNullOrEmpty(string text) =>
        new ReplySuggestion(text, PostbackData).GetErrors().Should().Contain("Text must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenTextExceedsMaxLength() =>
        new ReplySuggestion(new string('a', 26), PostbackData).GetErrors()
            .Should().Contain("Text must not exceed 25 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenPostbackDataIsNullOrEmpty(string postbackData) =>
        new ReplySuggestion(Text, postbackData).GetErrors().Should().Contain("PostbackData must not be null or empty.");

    private static ReplySuggestion BuildSuggestion() => new ReplySuggestion(Text, PostbackData);
}