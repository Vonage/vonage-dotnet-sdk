using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.RemoveStreamFromBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.RemoveStreamFromBroadcast
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid streamId;
        private readonly Guid broadcastId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<Guid>();
            this.streamId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
            RemoveStreamFromBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}/streams");
    }
}