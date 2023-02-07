using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.GetArchives;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.GetArchives
{
    public class GetArchivesRequestBuilderTest
    {
        private readonly Guid applicationId;

        public GetArchivesRequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            GetArchivesRequestBuilder.Build(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenCountIsHigherThanThreshold() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .WithCount(1001)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Count cannot be higher than 1000."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenCountIsNegative() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .WithCount(-1)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Count cannot be negative."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenOffsetIsNegative() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .WithOffset(-1)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Offset cannot be negative."));

        [Fact]
        public void Build_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .WithCount(100)
                .WithOffset(500)
                .WithSessionId("Some value")
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.Offset.Should().Be(500);
                    request.Count.Should().Be(100);
                    request.SessionId.Should().BeSome("Some value");
                });

        [Fact]
        public void Build_ShouldReturnSuccess_WithDefaultValues() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.Offset.Should().Be(0);
                    request.Count.Should().Be(50);
                    request.SessionId.Should().BeNone();
                });
    }
}