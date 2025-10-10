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
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void Constructor_ShouldSetUrl() => BuildSuggestion().Url.Should().Be(new Uri(Url));

    [Fact]
    public void Description_ShouldBeNull_GivenDefault() => BuildSuggestion().Description.Should().BeNull();

    [Fact]
    public void Type_ShouldBeOpenUrl() => BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void WithDescription_ShouldSetDescription() => BuildSuggestion()
        .WithDescription(Description).Description.Should().Be(Description);

    private static OpenUrlSuggestion BuildSuggestion() => new OpenUrlSuggestion(Text, PostbackData, new Uri(Url));
}