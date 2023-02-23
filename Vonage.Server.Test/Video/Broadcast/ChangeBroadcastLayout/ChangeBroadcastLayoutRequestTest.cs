using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Common;
using Vonage.Server.Video.Broadcast.ChangeBroadcastLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.ChangeBroadcastLayout
{
    public class ChangeBroadcastLayoutRequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid broadcastId;
        private readonly Layout layout;

        public ChangeBroadcastLayoutRequestTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<Guid>();
            this.layout = fixture.Create<Layout>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
            ChangeBroadcastLayoutRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithLayout(this.layout)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}/layout");
    }
}