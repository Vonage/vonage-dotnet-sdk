using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.UpdateTheme;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.UpdateTheme
{
    public class RequestBuilderTest
    {
        private readonly Color mainColor;
        private readonly Guid themeId;
        private readonly string themeName;
        private readonly string brandText;
        private readonly Uri shortCompanyUrl;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.themeId = fixture.Create<Guid>();
            this.brandText = fixture.Create<string>();
            this.mainColor = fixture.Create<Color>();
            this.themeName = fixture.Create<string>();
            this.shortCompanyUrl = fixture.Create<Uri>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            UpdateThemeRequest
                .Build()
                .WithThemeId(this.themeId)
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

        [Fact]
        public void Build_ShouldReturnFailure_GivenThemeIdIsEmpty() =>
            UpdateThemeRequest
                .Build()
                .WithThemeId(Guid.Empty)
                .Create()
                .Should()
                .BeParsingFailure("ThemeId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnSuccess() =>
            UpdateThemeRequest
                .Build()
                .WithThemeId(this.themeId)
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