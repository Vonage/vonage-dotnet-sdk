using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.CreateTheme;
using Vonage.Meetings.GetTheme;
using Xunit.Abstractions;

namespace Vonage.Integration.Test.Meetings;

public class MeetingsApiTest : BaseIntegrationTest
{
    [Fact]
    public async Task CreateTheme_ShouldReturnSuccess()
    {
        var request = this.BuildCreateThemeRequest();
        var createdTheme = await this.ApplicationClient.MeetingsClient.CreateThemeAsync(request);
        createdTheme.Should().BeSuccess(success =>
        {
            success.BrandText.Should().Be(request.BrandText);
            success.ThemeName.Should().Be(request.ThemeName.GetUnsafe());
            success.ShortCompanyUrl.Should().Be(request.ShortCompanyUrl.GetUnsafe());
            success.MainColor.ToArgb().Should().Be(request.MainColor.ToArgb());
        });
    }

    [Fact]
    public async Task CreateTheme_ShouldReturnSuccess_GivenThemeIsCreatedWithMandatoryValues()
    {
        var request = this.BuildCreateThemeRequestWithMandatoryValues();
        var createdTheme = await this.ApplicationClient.MeetingsClient.CreateThemeAsync(request);
        createdTheme.Should().BeSuccess(success =>
        {
            success.BrandText.Should().Be(request.BrandText);
            success.MainColor.ToArgb().Should().Be(request.MainColor.ToArgb());
        });
    }

    [Fact]
    public async Task GetTheme_ShouldRetrieveTheme_GivenThemeIsCreated()
    {
        var request = this.BuildCreateThemeRequest();
        var createdTheme = await this.ApplicationClient.MeetingsClient.CreateThemeAsync(request);
        var theme = createdTheme.IfFailure(failure => throw new Exception(failure.GetFailureMessage()));
        var getTheme = await this.ApplicationClient.MeetingsClient.GetThemeAsync(GetThemeRequest.Parse(theme.ThemeId));
        getTheme.Should().BeSuccess(success =>
        {
            success.ThemeId.Should().Be(theme.ThemeId);
            success.BrandText.Should().Be(request.BrandText);
            success.ThemeName.Should().Be(request.ThemeName.GetUnsafe());
            success.ShortCompanyUrl.Should().Be(request.ShortCompanyUrl.GetUnsafe());
            success.MainColor.ToArgb().Should().Be(request.MainColor.ToArgb());
        });
    }

    [Fact]
    public async Task GetTheme_ShouldRetrieveTheme_GivenThemeIsCreatedWithMandatoryValues()
    {
        var request = this.BuildCreateThemeRequestWithMandatoryValues();
        var createdTheme = await this.ApplicationClient.MeetingsClient.CreateThemeAsync(request);
        var theme = createdTheme.IfFailure(failure => throw new Exception(failure.GetFailureMessage()));
        var getTheme = await this.ApplicationClient.MeetingsClient.GetThemeAsync(GetThemeRequest.Parse(theme.ThemeId));
        getTheme.Should().BeSuccess(success =>
        {
            success.ThemeId.Should().Be(theme.ThemeId);
            success.BrandText.Should().Be(request.BrandText);
            success.MainColor.ToArgb().Should().Be(request.MainColor.ToArgb());
        });
    }

    private CreateThemeRequest BuildCreateThemeRequest() =>
        CreateThemeRequestBuilder
            .Build(this.Fixture.Create<string>(), this.Fixture.Create<Color>())
            .WithName(this.Fixture.Create<string>())
            .WithShortCompanyUrl(new Uri(this.Fixture.Create<string>(), UriKind.Relative))
            .Create()
            .GetSuccessUnsafe();

    private CreateThemeRequest BuildCreateThemeRequestWithMandatoryValues() =>
        CreateThemeRequestBuilder
            .Build(this.Fixture.Create<string>(), this.Fixture.Create<Color>())
            .Create()
            .GetSuccessUnsafe();
}