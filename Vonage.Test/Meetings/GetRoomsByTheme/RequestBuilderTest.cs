using System;
using AutoFixture;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Meetings.GetRoomsByTheme;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.GetRoomsByTheme;

public class RequestBuilderTest
{
    private readonly int endId;
    private readonly int pageSize;
    private readonly int startId;
    private readonly Guid themeId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.themeId = fixture.Create<Guid>();
        this.startId = fixture.Create<int>();
        this.endId = fixture.Create<int>();
        this.pageSize = fixture.Create<int>();
    }

    [Fact]
    public void Build_ShouldHaveDefaultValues() =>
        GetRoomsByThemeRequest
            .Build()
            .WithThemeId(this.themeId)
            .Create()
            .Should()
            .BeSuccess(success =>
            {
                success.ThemeId.Should().Be(this.themeId);
                success.StartId.Should().BeNone();
                success.EndId.Should().BeNone();
                success.PageSize.Should().BeNone();
            });

    [Property]
    public Property Build_ShouldReturnFailure_GivenPageSizeIsLowerThanZero() =>
        Prop.ForAll(
            Gen.Choose(-100, 0).ToArbitrary(),
            invalidPageSize => GetRoomsByThemeRequest
                .Build()
                .WithThemeId(this.themeId)
                .WithPageSize(invalidPageSize)
                .Create()
                .Should()
                .BeParsingFailure("PageSize cannot be lower than 1."));

    [Fact]
    public void Build_ShouldReturnFailure_GivenThemeIdIsNullOrWhitespace() =>
        GetRoomsByThemeRequest
            .Build()
            .WithThemeId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ThemeId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnSuccess() =>
        GetRoomsByThemeRequest
            .Build()
            .WithThemeId(this.themeId)
            .WithStartId(this.startId)
            .WithEndId(this.endId)
            .WithPageSize(this.pageSize)
            .Create()
            .Should()
            .BeSuccess(success =>
            {
                success.ThemeId.Should().Be(this.themeId);
                success.StartId.Should().BeSome(this.startId);
                success.EndId.Should().BeSome(this.endId);
                success.PageSize.Should().BeSome(this.pageSize);
            });
}