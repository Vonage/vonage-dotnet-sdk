using System;
using AutoFixture;
using Vonage.Meetings.DeleteTheme;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteTheme
{
    public class RequestTest
    {
        private readonly Guid themeId;

        public RequestTest() => this.themeId = new Fixture().Create<Guid>();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            DeleteThemeRequest.Build()
                .WithThemeId(this.themeId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/v1/meetings/themes/{this.themeId}");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithForceOption_GivenForceDelete() =>
            DeleteThemeRequest.Build()
                .WithThemeId(this.themeId)
                .WithForceDelete()
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/v1/meetings/themes/{this.themeId}?force=true");
    }
}