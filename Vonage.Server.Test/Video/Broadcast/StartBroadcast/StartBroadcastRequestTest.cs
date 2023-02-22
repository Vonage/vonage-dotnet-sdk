using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Common;
using Vonage.Server.Video.Broadcast.StartBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.StartBroadcast
{
    public class StartBroadcastRequestTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;

        public StartBroadcastRequestTest()
        {
            this.fixture = new Fixture();
            this.fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = this.fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StartBroadcastRequestBuilder.Build(this.applicationId)
                .WithSessionId(this.fixture.Create<string>())
                .WithLayout(this.fixture.Create<ArchiveLayout>())
                .WithOutputs(this.fixture.Create<StartBroadcastRequest.BroadcastOutput>())
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/broadcast");
    }
}