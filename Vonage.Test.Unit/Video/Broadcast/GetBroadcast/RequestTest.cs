using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Broadcast.GetBroadcast;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.GetBroadcast
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid broadcastId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
            GetBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}");
    }
}