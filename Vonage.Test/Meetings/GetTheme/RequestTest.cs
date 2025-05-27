#region
using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.GetTheme;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Meetings.GetTheme;

[Trait("Category", "Request")]
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
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v1/meetings/themes/{this.themeId}");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenThemeIdIsEmpty() =>
        GetThemeRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("ThemeId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        GetThemeRequest.Parse(this.themeId)
            .Should()
            .BeSuccess(request => request.ThemeId.Should().Be(this.themeId));
}