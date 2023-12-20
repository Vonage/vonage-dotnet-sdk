using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.StopBroadcast;
using Xunit;

namespace Vonage.Test.Video.Broadcast.StopBroadcast
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Guid broadcastId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            StopBroadcastRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithBroadcastId(this.broadcastId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenBroadcastIdIsEmpty() =>
            StopBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(Guid.Empty)
                .Create()
                .Should()
                .BeParsingFailure("BroadcastId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            StopBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ApplicationId.Should().Be(this.applicationId);
                    success.BroadcastId.Should().Be(this.broadcastId);
                });
    }
}