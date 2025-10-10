#region
using System;
using FluentAssertions;
using Vonage.Messages.Rcs.Suggestions;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs.Suggestions;

public class CreateCalendarEventSuggestionTest
{
    private const string Type = "create_calendar_event";
    private const string Text = "Action 1";
    private const string PostbackData = "action_1";
    private const string Title = "Title";
    private const string Description = "Description";
    private const string FallbackUrl = "https://example.com";
    private readonly DateTime endTime = DateTime.Now;
    private readonly DateTime startTime = DateTime.Now;

    [Fact]
    public void Constructor_ShouldSetEndTime() => this.BuildSuggestion().EndTime.Should().Be(this.endTime);

    [Fact]
    public void Constructor_ShouldSetPostbackData() => this.BuildSuggestion().PostbackData.Should().Be(PostbackData);

    [Fact]
    public void Constructor_ShouldSetStartTime() => this.BuildSuggestion().StartTime.Should().Be(this.startTime);

    [Fact]
    public void Constructor_ShouldSetText() => this.BuildSuggestion().Text.Should().Be(Text);

    [Fact]
    public void Constructor_ShouldSetTitle() => this.BuildSuggestion().Title.Should().Be(Title);

    [Fact]
    public void Description_ShouldBeNull_GivenDefault() => this.BuildSuggestion().Description.Should().BeNull();

    [Fact]
    public void FallbackUrl_ShouldBeNull_GivenDefault() => this.BuildSuggestion().FallbackUrl.Should().BeNull();

    [Fact]
    public void Type_ShouldBeCreateCalendarEvent() => this.BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void WithDescription_ShouldSetDescription() =>
        this.BuildSuggestion().WithDescription(Description).Description.Should().Be(Description);

    [Fact]
    public void WithFallbackUrl_ShouldSetFallbackUrl() =>
        this.BuildSuggestion().WithFallbackUrl(new Uri(FallbackUrl)).FallbackUrl.Should().Be(new Uri(FallbackUrl));

    private CreateCalendarEventSuggestion BuildSuggestion() =>
        new CreateCalendarEventSuggestion(Text, PostbackData, this.startTime, this.endTime, Title);
}