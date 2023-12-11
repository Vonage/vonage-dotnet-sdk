using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Sip.PlayToneIntoCall;
using Xunit;

namespace Vonage.Test.Unit.Video.Sip.PlayToneIntoCall
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;
        private readonly string digits;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.digits = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            PlayToneIntoCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithDigits(this.digits)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/play-dtmf");
    }
}