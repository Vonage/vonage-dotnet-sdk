#region
using System;
using FluentAssertions;
using Vonage.Messages.Rcs.Suggestions;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs.Suggestions;

public class OpenUrlSuggestionTest
{
    private const string Type = "open_url";
    private const string Text = "Action 1";
    private const string PostbackData = "action_1";
    private const string Url = "https://example.com";
    private const string Description = "Description";

    [Fact]
    public void Constructor_ShouldSetDescription() => BuildSuggestion().Description.Should().Be(Description);

    [Fact]
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void Constructor_ShouldSetUrl() => BuildSuggestion().Url.Should().Be(new Uri(Url));

    [Fact]
    public void Type_ShouldBeOpenUrl() => BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void GetErrors_ReturnsEmpty_WhenValid() => BuildSuggestion().GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenTextIsNullOrEmpty(string text) =>
        new OpenUrlSuggestion(text, PostbackData, new Uri(Url), Description).GetErrors()
            .Should().Contain("Text must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenTextExceedsMaxLength() =>
        new OpenUrlSuggestion(new string('a', 26), PostbackData, new Uri(Url), Description).GetErrors()
            .Should().Contain("Text must not exceed 25 characters.");

    [Fact]
    public void GetErrors_ReturnsError_WhenUrlIsNull() =>
        new OpenUrlSuggestion(Text, PostbackData, null, Description).GetErrors()
            .Should().Contain("Url must not be null.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenDescriptionIsNullOrEmpty(string description) =>
        new OpenUrlSuggestion(Text, PostbackData, new Uri(Url), description).GetErrors()
            .Should().Contain("Description must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenDescriptionExceedsMaxLength() =>
        new OpenUrlSuggestion(Text, PostbackData, new Uri(Url), new string('a', 501)).GetErrors()
            .Should().Contain("Description must not exceed 500 characters.");

    private static OpenUrlSuggestion BuildSuggestion() =>
        new OpenUrlSuggestion(Text, PostbackData, new Uri(Url), Description);
}