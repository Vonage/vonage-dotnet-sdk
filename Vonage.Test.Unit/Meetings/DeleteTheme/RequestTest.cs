using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.DeleteTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteTheme
{
    public class RequestTest
    {
        private readonly Guid themeId;

        public RequestTest() => this.themeId = new Fixture().Create<Guid>();

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

        [Fact]
        public void Parse_ShouldReturnFailure_GivenRecordingIdIsEmpty() =>
            DeleteThemeRequest.Parse(Guid.Empty, false)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            DeleteThemeRequest.Parse(this.themeId, false)
                .Should()
                .BeSuccess(request => request.ThemeId.Should().Be(this.themeId));
    }
}