using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Signaling;
using Vonage.Server.Video.Signaling.SendSignal;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignal
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly SignalContent content;
        private readonly string connectionId;
        private readonly string sessionId;

        public RequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<Guid>();
            this.sessionId = this.fixture.Create<string>();
            this.connectionId = this.fixture.Create<string>();
            this.content = this.fixture.Create<SignalContent>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(this.content)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/v2/project/{this.applicationId}/session/{this.sessionId}/connection/{this.connectionId}/signal");
    }
}