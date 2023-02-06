using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetTheme
{
    public class GetThemeRequestTest
    {
        private readonly string themeId;

        public GetThemeRequestTest()
        {
            var fixture = new Fixture();
            this.themeId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetThemeRequest.Parse(this.themeId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/themes/{this.themeId}");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace(string value) =>
            GetThemeRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetThemeRequest.Parse(this.themeId)
                .Should()
                .BeSuccess(request => request.ThemeId.Should().Be(this.themeId));
    }
}