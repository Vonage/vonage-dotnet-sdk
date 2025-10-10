#region
using FluentAssertions;
using Vonage.Messages.Rcs.Suggestions;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs.Suggestions;

public class ShareLocationSuggestionTest
{
    private const string Type = "share_location";
    private const string Text = "Action 1";
    private const string PostbackData = "action_1";

    [Fact]
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void Type_ShouldBeShareLocation() => BuildSuggestion().Type.Should().Be(Type);

    private static ShareLocationSuggestion BuildSuggestion() => new ShareLocationSuggestion(Text, PostbackData);
}