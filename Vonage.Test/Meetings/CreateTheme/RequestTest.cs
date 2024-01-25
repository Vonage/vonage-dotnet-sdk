using System.Drawing;
using AutoFixture;
using Vonage.Meetings.CreateTheme;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.CreateTheme;

public class RequestTest
{
    private readonly string displayName;
    private readonly Color mainColor;

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