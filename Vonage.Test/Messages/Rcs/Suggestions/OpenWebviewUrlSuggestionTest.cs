#region
using System;
using FluentAssertions;
using Vonage.Messages.Rcs.Suggestions;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs.Suggestions;

public class OpenWebviewUrlSuggestionTest
{
    private const string Type = "open_url_in_webview";
    private const string Text = "Action 1";
    private const string PostbackData = "action_1";
    private const string Url = "https://example.com";
    private const string Description = "Description";
    private const OpenWebviewUrlSuggestion.ViewModeValue ViewMode = OpenWebviewUrlSuggestion.ViewModeValue.Full;

    [Fact]
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void Constructor_ShouldSetUrl() => BuildSuggestion().Url.Should().Be(new Uri(Url));

    [Fact]
    public void Description_ShouldBeNull_GivenDefault() => BuildSuggestion().Description.Should().BeNull();

    [Fact]
    public void Type_ShouldBeOpenWebviewUrl() => BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void ViewMode_ShouldBeNull_GivenDefault() => BuildSuggestion().ViewMode.Should().BeNull();

    [Fact]
    public void WithDescription_ShouldSetDescription() => BuildSuggestion()
        .WithDescription(Description).Description.Should().Be(Description);

    [Fact]
    public void WithViewMode_ShouldSetViewMode() => BuildSuggestion()
        .WithViewMode(ViewMode).ViewMode.Should().Be(ViewMode);

    private static OpenWebviewUrlSuggestion BuildSuggestion() =>
        new OpenWebviewUrlSuggestion(Text, PostbackData, new Uri(Url));
}