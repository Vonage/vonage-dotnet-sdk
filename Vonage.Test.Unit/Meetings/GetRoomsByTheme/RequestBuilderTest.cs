using System;
using AutoFixture;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRoomsByTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoomsByTheme
{
    public class RequestBuilderTest
    {
        private readonly Guid themeId;
        private readonly int startId;
        private readonly int endId;
        private readonly int pageSize;

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
                    .BeFailure(ResultFailure.FromErrorMessage("PageSize cannot be lower than 1.")));

        [Fact]
        public void Build_ShouldReturnFailure_GivenThemeIdIsNullOrWhitespace() =>
            GetRoomsByThemeRequest
                .Build()
                .WithThemeId(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be empty."));

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
}