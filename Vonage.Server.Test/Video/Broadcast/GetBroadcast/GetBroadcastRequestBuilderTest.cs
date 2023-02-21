using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.GetBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcast
{
    public class GetBroadcastRequestBuilderTest
    {
        private readonly Guid applicationId;

        private readonly string broadcastId;

        public GetBroadcastRequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            GetBroadcastRequestBuilder.Build()
                .WithApplicationId(Guid.Empty)
                .WithBroadcastId(this.broadcastId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenBroadcastIdIsNullOrWhitespace(string value) =>
            GetBroadcastRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(value)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("BroadcastId cannot be null or whitespace."));

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