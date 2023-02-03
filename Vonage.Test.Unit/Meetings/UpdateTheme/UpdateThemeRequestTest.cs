﻿using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateTheme
{
    public class UpdateThemeRequestTest
    {
        private readonly string themeId;

        public UpdateThemeRequestTest()
        {
            var fixture = new Fixture();
            this.themeId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            UpdateThemeRequestBuilder
                .Build(this.themeId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/themes/{this.themeId}");
    }
}