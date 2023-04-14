using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.GetBroadcasts;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcasts
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldAssignCount_GivenWithCountIsUsed() =>
            GetBroadcastsRequestBuilder.Build(this.applicationId)
                .WithCount(100)
                .Create()
                .Should()
                .BeSuccess(request => request.Count.Should().Be(100));

        [Fact]
        public void Build_ShouldAssignOffset_GivenWithOffsetIsUsed() =>
            GetBroadcastsRequestBuilder.Build(this.applicationId)
                .WithOffset(500)
                .Create()
                .Should()
                .BeSuccess(request => request.Offset.Should().Be(500));

        [Fact]
        public void Build_ShouldAssignSessionId_GivenWithSessionIdIsUsed() =>
            GetBroadcastsRequestBuilder.Build(this.applicationId)
                .WithSessionId("some value")
                .Create()
                .Should()
                .BeSuccess(request => request.SessionId.Should().BeSome("some value"));

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            GetBroadcastsRequestBuilder.Build(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenCountIsHigherThanThreshold() =>
            GetBroadcastsRequestBuilder.Build(this.applicationId)
                .WithCount(1001)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Count cannot be higher than 1000."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenCountIsNegative() =>
            GetBroadcastsRequestBuilder.Build(this.applicationId)
                .WithCount(-1)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Count cannot be negative."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenOffsetIsNegative() =>
            GetBroadcastsRequestBuilder.Build(this.applicationId)
                .WithOffset(-1)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Offset cannot be negative."));

        [Fact]
        public void Build_ShouldReturnSuccess_WithDefaultValues() =>
            GetBroadcastsRequestBuilder.Build(this.applicationId)
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