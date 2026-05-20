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

    [Fact]
    public void GetErrors_ReturnsEmpty_WhenValid() => BuildSuggestion().GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenTextIsNullOrEmpty(string text) =>
        new DialSuggestion(text, PostbackData, PhoneNumber).GetErrors().Should().Contain("Text must not be null or empty.");

    [Fact]
    public void GetErrors_ReturnsError_WhenTextExceedsMaxLength() =>
        new DialSuggestion(new string('a', 26), PostbackData, PhoneNumber).GetErrors()
            .Should().Contain("Text must not exceed 25 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenPostbackDataIsNullOrEmpty(string postbackData) =>
        new DialSuggestion(Text, postbackData, PhoneNumber).GetErrors()
            .Should().Contain("PostbackData must not be null or empty.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenPhoneNumberIsNullOrEmpty(string phoneNumber) =>
        new DialSuggestion(Text, PostbackData, phoneNumber).GetErrors()
            .Should().Contain("PhoneNumber must not be null or empty.");

    [Theory]
    [InlineData("0123456789")]
    [InlineData("1")]
    [InlineData("abc")]
    public void GetErrors_ReturnsError_WhenPhoneNumberIsInvalidFormat(string phoneNumber) =>
        new DialSuggestion(Text, PostbackData, phoneNumber).GetErrors()
            .Should().Contain("PhoneNumber must be in E.164 format.");

    [Theory]
    [InlineData("+33123456789")]
    [InlineData("14155550100")]
    public void GetErrors_ReturnsEmpty_WhenPhoneNumberIsValidE164(string phoneNumber) =>
        new DialSuggestion(Text, PostbackData, phoneNumber).GetErrors().Should().BeEmpty();

    private static DialSuggestion BuildSuggestion() => new DialSuggestion(Text, PostbackData, PhoneNumber);
}