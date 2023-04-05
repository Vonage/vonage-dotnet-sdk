using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.CreateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateTheme
{
    public class RequestBuilderTest
    {
        private readonly Color mainColor;
        private readonly string themeName;
        private readonly string brandText;
        private readonly Uri shortCompanyUrl;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.brandText = fixture.Create<string>();
            this.mainColor = fixture.Create<Color>();
            this.themeName = fixture.Create<string>();
            this.shortCompanyUrl = fixture.Create<Uri>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            CreateThemeRequestBuilder
                .Build(this.brandText, this.mainColor)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.BrandText.Should().Be(this.brandText);
                    success.MainColor.Should().Be(this.mainColor);
                    success.ThemeName.Should().BeNone();
                    success.ShortCompanyUrl.Should().BeNone();
                });

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenBrandTextIsNullOrWhitespace(string invalidBrandText) =>
            CreateThemeRequestBuilder
                .Build(invalidBrandText, this.mainColor)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("BrandText cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess() =>
            CreateThemeRequestBuilder
                .Build(this.brandText, this.mainColor)
                .WithName(this.themeName)
                .WithShortCompanyUrl(this.shortCompanyUrl)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.BrandText.Should().Be(this.brandText);
                    success.MainColor.Should().Be(this.mainColor);
                    success.ThemeName.Should().BeSome(this.themeName);
                    success.ShortCompanyUrl.Should().BeSome(this.shortCompanyUrl);
                });
    }
}