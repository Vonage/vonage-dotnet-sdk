using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.InitiateCall;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.InitiateCall
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly SipElement sip;
        private readonly string sessionId;
        private readonly string token;

        public RequestTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.token = fixture.Create<string>();
            this.sip = fixture.Create<SipElement>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            InitiateCallRequest.Parse(this.applicationId, this.sessionId, this.token, this.sip)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/dial");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            InitiateCallRequest.Parse(Guid.Empty, this.sessionId, this.token, this.sip)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            InitiateCallRequest.Parse(this.applicationId, value, this.token, this.sip)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenTokenIsNullOrWhitespace(string value) =>
            InitiateCallRequest.Parse(this.applicationId, this.sessionId, value, this.sip)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Token cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            InitiateCallRequest.Parse(this.applicationId, this.sessionId, this.token, this.sip)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Token.Should().Be(this.token);
                    request.Sip.Should().Be(this.sip);
                });
    }
}