#region
using System;
using FluentAssertions;
using Vonage.Messages.Rcs.Suggestions;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs.Suggestions;

public class DialSuggestionTest
{
    private const string Type = "dial";
    private const string Text = "Action 1";
    private const string PostbackData = "action_1";
    private const string PhoneNumber = "+33123456789";
    private const string FallbackUrl = "https://example.com";

    [Fact]
    public void Constructor_ShouldSetPhoneNumber() => BuildSuggestion().PhoneNumber.Should().Be(PhoneNumber);

    [Fact]
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void FallbackUrl_ShouldBeNull_GivenDefault() => BuildSuggestion().FallbackUrl.Should().BeNull();

    [Fact]
    public void Type_ShouldBeDial() => BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void WithFallbackUrl_ShouldSetFallbackUrl() => BuildSuggestion()
        .WithFallbackUrl(new Uri(FallbackUrl)).FallbackUrl.Should().Be(new Uri(FallbackUrl));

    private static DialSuggestion BuildSuggestion() => new DialSuggestion(Text, PostbackData, PhoneNumber);
}