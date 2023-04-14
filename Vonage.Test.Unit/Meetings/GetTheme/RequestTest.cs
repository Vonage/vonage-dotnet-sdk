using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetTheme
{
    public class RequestTest
    {
        private readonly Guid themeId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.themeId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetThemeRequest.Parse(this.themeId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/themes/{this.themeId}");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenThemeIdIsEmpty() =>
            GetThemeRequest.Parse(Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetThemeRequest.Parse(this.themeId)
                .Should()
                .BeSuccess(request => request.ThemeId.Should().Be(this.themeId));
    }
}