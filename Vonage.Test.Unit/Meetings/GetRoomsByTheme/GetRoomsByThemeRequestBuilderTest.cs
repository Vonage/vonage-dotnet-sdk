using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRoomsByTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoomsByTheme
{
    public class GetRoomsByThemeRequestBuilderTest
    {
        private readonly string themeId;
        private readonly string startId;
        private readonly string endId;

        public GetRoomsByThemeRequestBuilderTest()
        {
            var fixture = new Fixture();
            this.themeId = fixture.Create<string>();
            this.startId = fixture.Create<string>();
            this.endId = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            GetRoomsByThemeVonageRequestBuilder
                .Build(this.themeId)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ThemeId.Should().Be(this.themeId);
                    success.StartId.Should().BeNone();
                    success.EndId.Should().BeNone();
                });

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenThemeIdIsNullOrWhitespace(string invalidThemeId) =>
            GetRoomsByThemeVonageRequestBuilder
                .Build(invalidThemeId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess() =>
            GetRoomsByThemeVonageRequestBuilder
                .Build(this.themeId)
                .WithStartId(this.startId)
                .WithEndId(this.endId)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ThemeId.Should().Be(this.themeId);
                    success.StartId.Should().BeSome(this.startId);
                    success.EndId.Should().BeSome(this.endId);
                });
    }
}