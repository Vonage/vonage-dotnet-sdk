using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.CreateTheme;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.CreateTheme;

public class RequestBuilderTest
{
    private readonly string brandText;
    private readonly Color mainColor;
    private readonly Uri shortCompanyUrl;
    private readonly string themeName;

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
        CreateThemeRequest
            .Build()
            .WithBrand(this.brandText)
            .WithColor(this.mainColor)
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
        CreateThemeRequest
            .Build()
            .WithBrand(invalidBrandText)
            .WithColor(this.mainColor)
            .Create()
            .Should()
            .BeParsingFailure("BrandText cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnSuccess() =>
        CreateThemeRequest
            .Build()
            .WithBrand(this.brandText)
            .WithColor(this.mainColor)
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