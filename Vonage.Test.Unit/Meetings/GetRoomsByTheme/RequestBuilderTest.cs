using System;
using AutoFixture;
using FluentAssertions;
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

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.themeId = fixture.Create<Guid>();
            this.startId = fixture.Create<int>();
            this.endId = fixture.Create<int>();
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
                });

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
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ThemeId.Should().Be(this.themeId);
                    success.StartId.Should().BeSome(this.startId);
                    success.EndId.Should().BeSome(this.endId);
                });
    }
}