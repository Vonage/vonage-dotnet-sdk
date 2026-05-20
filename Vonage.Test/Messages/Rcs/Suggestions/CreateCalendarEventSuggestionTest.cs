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
    public void Constructor_ShouldSetDescription() => this.BuildSuggestion().Description.Should().Be(Description);

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
    public void FallbackUrl_ShouldBeNull_GivenDefault() => this.BuildSuggestion().FallbackUrl.Should().BeNull();

    [Fact]
    public void Type_ShouldBeCreateCalendarEvent() => this.BuildSuggestion().Type.Should().Be(Type);

    [Fact]
    public void WithFallbackUrl_ShouldSetFallbackUrl() =>
        this.BuildSuggestion().WithFallbackUrl(new Uri(FallbackUrl)).FallbackUrl.Should().Be(new Uri(FallbackUrl));

    [Fact]
    public void GetErrors_ReturnsEmpty_WhenValid() => this.BuildSuggestion().GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenTextIsNullOrEmpty(string text) =>
        new CreateCalendarEventSuggestion(text, PostbackData, this.startTime, this.endTime, Title, Description)
            .GetErrors().Should().Contain("Text must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenTextExceedsMaxLength() =>
        new CreateCalendarEventSuggestion(new string('a', 26), PostbackData, this.startTime, this.endTime, Title,
                Description)
            .GetErrors().Should().Contain("Text must not exceed 25 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenTitleIsNullOrEmpty(string title) =>
        new CreateCalendarEventSuggestion(Text, PostbackData, this.startTime, this.endTime, title, Description)
            .GetErrors().Should().Contain("Title must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenTitleExceedsMaxLength() =>
        new CreateCalendarEventSuggestion(Text, PostbackData, this.startTime, this.endTime, new string('a', 101),
                Description)
            .GetErrors().Should().Contain("Title must not exceed 100 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenDescriptionIsNullOrEmpty(string description) =>
        new CreateCalendarEventSuggestion(Text, PostbackData, this.startTime, this.endTime, Title, description)
            .GetErrors().Should().Contain("Description must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenDescriptionExceedsMaxLength() =>
        new CreateCalendarEventSuggestion(Text, PostbackData, this.startTime, this.endTime, Title,
                new string('a', 501))
            .GetErrors().Should().Contain("Description must not exceed 500 characters.");

    private CreateCalendarEventSuggestion BuildSuggestion() =>
        new CreateCalendarEventSuggestion(Text, PostbackData, this.startTime, this.endTime, Title, Description);
}