using System.Drawing;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.CreateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateTheme
{
    public class RequestTest
    {
        private readonly Color mainColor;
        private readonly string displayName;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.displayName = fixture.Create<string>();
            this.mainColor = fixture.Create<Color>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateThemeRequest
                .Build()
                .WithBrand(this.displayName)
                .WithColor(this.mainColor)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v1/meetings/themes");
    }
}