using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.PlayToneIntoConnection;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.PlayToneIntoConnection
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;
        private readonly string connectionId;
        private readonly string digits;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.connectionId = fixture.Create<string>();
            this.digits = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            PlayToneIntoConnectionRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithDigits(this.digits)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/v2/project/{this.applicationId}/session/{this.sessionId}/connection/{this.connectionId}/play-dtmf");
    }
}