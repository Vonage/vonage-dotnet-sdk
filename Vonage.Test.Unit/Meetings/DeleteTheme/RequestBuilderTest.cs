using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.DeleteTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteTheme
{
    public class RequestBuilderTest
    {
        private readonly Guid themeId;

        public RequestBuilderTest() => this.themeId = new Fixture().Create<Guid>();

        [Fact]
        public void Parse_ShouldReturnDefaultValueForForceDelete() =>
            DeleteThemeRequest.Build()
                .WithThemeId(this.themeId)
                .Create()
                .Map(request => request.ForceDelete)
                .Should()
                .BeSuccess(false);

        [Fact]
        public void Parse_ShouldReturnFailure_GivenRecordingIdIsEmpty() =>
            DeleteThemeRequest.Build()
                .WithThemeId(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ThemeId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSetThemeId() =>
            DeleteThemeRequest.Build()
                .WithThemeId(this.themeId)
                .Create()
                .Should()
                .BeSuccess(request => request.ThemeId.Should().Be(this.themeId));

        [Fact]
        public void Parse_ShouldSetForceDelete_GivenWithForceDeleteIsUsed() =>
            DeleteThemeRequest.Build()
                .WithThemeId(this.themeId)
                .WithForceDelete()
                .Create()
                .Map(request => request.ForceDelete)
                .Should()
                .BeSuccess(true);
    }
}