using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateTheme
{
    public class UpdateThemeRequestBuilderTest
    {
        private readonly Color mainColor;
        private readonly string themeName;
        private readonly string brandText;
        private readonly string themeId;
        private readonly Uri shortCompanyUrl;

        public UpdateThemeRequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.themeId = fixture.Create<string>();
            this.brandText = fixture.Create<string>();
            this.mainColor = fixture.Create<Color>();
            this.themeName = fixture.Create<string>();
            this.shortCompanyUrl = fixture.Create<Uri>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            UpdateThemeRequestBuilder
                .Build(this.themeId)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ThemeId.Should().Be(this.themeId);
                    success.BrandText.Should().BeNone();
                    success.MainColor.Should().BeNone();
                    success.ThemeName.Should().BeNone();
                    success.ShortCompanyUrl.Should().BeNone();
                });

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenThemeIdIsNullOrWhitespace(string invalidThemeId) =>
            UpdateThemeRequestBuilder
                .Build(invalidThemeId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess() =>
            UpdateThemeRequestBuilder
                .Build(this.themeId)
                .WithColor(this.mainColor)
                .WithBrandText(this.brandText)
                .WithName(this.themeName)
                .WithShortCompanyUrl(this.shortCompanyUrl)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ThemeId.Should().Be(this.themeId);
                    success.BrandText.Should().BeSome(this.brandText);
                    success.MainColor.Should().BeSome(this.mainColor);
                    success.ThemeName.Should().BeSome(this.themeName);
                    success.ShortCompanyUrl.Should().BeSome(this.shortCompanyUrl);
                });
    }
}