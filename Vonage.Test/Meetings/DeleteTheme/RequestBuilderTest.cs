using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.DeleteTheme;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.DeleteTheme;

public class RequestBuilderTest
{
    private readonly Guid themeId;

    public RequestBuilderTest() => this.themeId = new Fixture().Create<Guid>();

    [Fact]
    public void Build_ShouldReturnDefaultValueForForceDelete() =>
        DeleteThemeRequest.Build()
            .WithThemeId(this.themeId)
            .Create()
            .Map(request => request.ForceDelete)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldReturnFailure_GivenRecordingIdIsEmpty() =>
        DeleteThemeRequest.Build()
            .WithThemeId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ThemeId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnSetThemeId() =>
        DeleteThemeRequest.Build()
            .WithThemeId(this.themeId)
            .Create()
            .Should()
            .BeSuccess(request => request.ThemeId.Should().Be(this.themeId));

    [Fact]
    public void Build_ShouldSetForceDelete_GivenWithForceDeleteIsUsed() =>
        DeleteThemeRequest.Build()
            .WithThemeId(this.themeId)
            .WithForceDelete()
            .Create()
            .Map(request => request.ForceDelete)
            .Should()
            .BeSuccess(true);
}