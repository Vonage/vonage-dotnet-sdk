﻿using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateApplication;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateApplication
{
    public class UpdateApplicationRequestTest
    {
        private readonly string themeId;

        public UpdateApplicationRequestTest()
        {
            var fixture = new Fixture();
            this.themeId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            UpdateApplicationRequest.Parse(this.themeId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/beta/meetings/applications");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenThemeIdIsNullOrWhitespace(string value) =>
            UpdateApplicationRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("DefaultThemeId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            UpdateApplicationRequest.Parse(this.themeId)
                .Should()
                .BeSuccess(request => request.DefaultThemeId.Should().Be(this.themeId));
    }
}