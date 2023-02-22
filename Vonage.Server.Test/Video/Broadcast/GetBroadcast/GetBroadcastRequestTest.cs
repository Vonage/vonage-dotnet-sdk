﻿using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.GetBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcast
{
    public class GetBroadcastRequestTest
    {
        private readonly Guid applicationId;
        private readonly string broadcastId;

        public GetBroadcastRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
            GetBroadcastRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}");
    }
}