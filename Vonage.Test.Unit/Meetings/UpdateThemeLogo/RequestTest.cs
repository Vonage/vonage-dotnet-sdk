using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateThemeLogo;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateThemeLogo
{
    public class RequestTest
    {
        private readonly Guid themeId;
        private readonly string filePath;
        private readonly ThemeLogoType logoType;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.themeId = fixture.Create<Guid>();
            this.filePath = fixture.Create<string>();
            this.logoType = fixture.Create<ThemeLogoType>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenFilepathIsNullOrWhitespace(string value) =>
            UpdateThemeLogoRequest.Parse(this.themeId, this.logoType, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("FilePath cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenThemeIdIsEmpty() =>
            UpdateThemeLogoRequest.Parse(Guid.Empty, this.logoType, this.filePath)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            UpdateThemeLogoRequest.Parse(this.themeId, this.logoType, this.filePath)
                .Should()
                .BeSuccess(request =>
                {
                    request.Type.Should().Be(this.logoType);
                    request.ThemeId.Should().Be(this.themeId);
                    request.FilePath.Should().Be(this.filePath);
                });
    }
}