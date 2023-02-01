using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.DeleteTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteTheme
{
    public class DeleteThemeRequestTest
    {
        private readonly string themeId;

        public DeleteThemeRequestTest() => this.themeId = new Fixture().Create<string>();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            DeleteThemeRequest.Parse(this.themeId, false)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/beta/meetings/themes/{this.themeId}");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithForceOption_GivenForceIsTrue() =>
            DeleteThemeRequest.Parse(this.themeId, true)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/beta/meetings/themes/{this.themeId}?force=true");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenRecordingIdIsNullOrWhitespace(string value) =>
            DeleteThemeRequest.Parse(value, false)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            DeleteThemeRequest.Parse(this.themeId, false)
                .Should()
                .BeSuccess(request => request.ThemeId.Should().Be(this.themeId));
    }
}