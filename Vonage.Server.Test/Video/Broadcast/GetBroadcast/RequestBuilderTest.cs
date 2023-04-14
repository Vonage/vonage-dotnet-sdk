using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.GetBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcast
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
            GetBroadcastRequestBuilder.Build()
                .WithApplicationId(Guid.Empty)
                .WithBroadcastId(this.broadcastId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenBroadcastIdIsEmpty() =>
            GetBroadcastRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("BroadcastId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetBroadcastRequestBuilder.Build()
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