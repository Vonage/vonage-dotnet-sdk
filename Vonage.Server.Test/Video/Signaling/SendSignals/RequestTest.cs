using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Signaling;
using Vonage.Server.Video.Signaling.SendSignals;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignals
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly SignalContent content;
        private readonly string sessionId;

        public RequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<Guid>();
            this.sessionId = this.fixture.Create<string>();
            this.content = this.fixture.Create<SignalContent>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            SendSignalsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithContent(this.content)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/signal");
    }
}