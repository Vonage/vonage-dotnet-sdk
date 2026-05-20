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
    public void Constructor_ShouldSetDescription() => BuildSuggestion().Description.Should().Be(Description);

    [Fact]
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void Constructor_ShouldSetUrl() => BuildSuggestion().Url.Should().Be(new Uri(Url));

    [Fact]
    public void Type_ShouldBeOpenWebviewUrl() => BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void ViewMode_ShouldBeNull_GivenDefault() => BuildSuggestion().ViewMode.Should().BeNull();

    [Fact]
    public void WithViewMode_ShouldSetViewMode() => BuildSuggestion()
        .WithViewMode(ViewMode).ViewMode.Should().Be(ViewMode);

    [Fact]
    public void GetErrors_ReturnsEmpty_WhenValid() => BuildSuggestion().GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenTextIsNullOrEmpty(string text) =>
        new OpenWebviewUrlSuggestion(text, PostbackData, new Uri(Url), Description).GetErrors()
            .Should().Contain("Text must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenTextExceedsMaxLength() =>
        new OpenWebviewUrlSuggestion(new string('a', 26), PostbackData, new Uri(Url), Description).GetErrors()
            .Should().Contain("Text must not exceed 25 characters.");

    [Fact]
    public void GetErrors_ReturnsError_WhenUrlIsNull() =>
        new OpenWebviewUrlSuggestion(Text, PostbackData, null, Description).GetErrors()
            .Should().Contain("Url must not be null.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenDescriptionIsNullOrEmpty(string description) =>
        new OpenWebviewUrlSuggestion(Text, PostbackData, new Uri(Url), description).GetErrors()
            .Should().Contain("Description must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenDescriptionExceedsMaxLength() =>
        new OpenWebviewUrlSuggestion(Text, PostbackData, new Uri(Url), new string('a', 501)).GetErrors()
            .Should().Contain("Description must not exceed 500 characters.");

    private static OpenWebviewUrlSuggestion BuildSuggestion() =>
        new OpenWebviewUrlSuggestion(Text, PostbackData, new Uri(Url), Description);
}