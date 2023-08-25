using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateTheme
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
            UpdateThemeRequest
                .Build()
                .WithThemeId(this.themeId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v1/meetings/themes/{this.themeId}");
    }
}