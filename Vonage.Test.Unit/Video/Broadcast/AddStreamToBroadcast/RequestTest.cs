﻿using System;
using AutoFixture;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Broadcast.AddStreamToBroadcast;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.AddStreamToBroadcast
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
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}/streams");
    }
}