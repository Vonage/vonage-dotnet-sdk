using System.Drawing;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.CreateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateTheme
{
    public class CreateThemeRequestTest
    {
        private readonly Color mainColor;
        private readonly string displayName;

        public CreateThemeRequestTest()
        {
            var fixture = new Fixture();
            this.displayName = fixture.Create<string>();
            this.mainColor = fixture.Create<Color>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateThemeVonageRequestBuilder
                .Build(this.displayName, this.mainColor)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/beta/meetings/themes");
    }
}