#region
using System;
using FluentAssertions;
using Vonage.Messages.Rcs.Suggestions;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs.Suggestions;

public class ViewLocationSuggestionTest
{
    private const string Type = "view_location";
    private const string Text = "Action 1";
    private const string PostbackData = "action_1";
    private const string Latitude = "37.7749";
    private const string Longitude = "-122.4194";
    private const string PinLabel = "vonage";
    private const string FallbackUrl = "https://example.com";

    [Fact]
    public void Constructor_ShouldSetLatitude() => BuildSuggestion().Latitude.Should().Be(Latitude);

    [Fact]
    public void Constructor_ShouldSetLongitude() => BuildSuggestion().Longitude.Should().Be(Longitude);

    [Fact]
    public void Constructor_ShouldSetPinLabel() => BuildSuggestion().PinLabel.Should().Be(PinLabel);

    [Fact]
    public void Constructor_ShouldSetPostbackData() => BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetText() => BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void FallbackUrl_ShouldBeNull_GivenDefault() => BuildSuggestion().FallbackUrl.Should().BeNull();

    [Fact]
    public void Type_ShouldBeViewLocation() => BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void WithFallbackUrl_ShouldSetFallbackUrl() => BuildSuggestion()
        .WithFallbackUrl(new Uri(FallbackUrl)).FallbackUrl.Should().Be(new Uri(FallbackUrl));

    private static ViewLocationSuggestion BuildSuggestion() =>
        new ViewLocationSuggestion(Text, PostbackData, Latitude, Longitude, PinLabel);
}