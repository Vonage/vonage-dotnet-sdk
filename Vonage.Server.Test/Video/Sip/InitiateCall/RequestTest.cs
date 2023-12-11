using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Sip.InitiateCall;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.InitiateCall
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly SipElement sip;
        private readonly string sessionId;
        private readonly string token;
        private readonly Uri uri;

        public RequestTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.token = fixture.Create<string>();
            this.sip = fixture.Create<SipElement>();
            this.uri = fixture.Create<Uri>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            InitiateCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithToken(this.token)
                .WithSipUri(this.uri)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/dial");
    }
}