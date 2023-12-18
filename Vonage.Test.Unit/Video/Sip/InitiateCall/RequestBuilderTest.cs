using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Sip.InitiateCall;
using Xunit;

namespace Vonage.Test.Unit.Video.Sip.InitiateCall
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly SipElement.SipAuthentication authentication;
        private readonly string sessionId;
        private readonly string token;
        private readonly string from;
        private readonly Uri uri;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.token = fixture.Create<string>();
            this.uri = fixture.Create<Uri>();
            this.from = fixture.Create<string>();
            this.authentication = new SipElement.SipAuthentication(fixture.Create<string>(), fixture.Create<string>());
        }

        [Fact]
        public void Build_ShouldEnableEncryptedMedia_GivenEnableEncryptedMediaIsUsed() =>
            this.BuildBaseRequest()
                .EnableEncryptedMedia()
                .Create()
                .Map(request => request.Sip)
                .Should()
                .BeSuccess(success => success.HasEncryptedMedia.Should().BeTrue());

        [Fact]
        public void Build_ShouldEnableForceMute_GivenEnableForceMuteIsUsed() =>
            this.BuildBaseRequest()
                .EnableForceMute()
                .Create()
                .Map(request => request.Sip)
                .Should()
                .BeSuccess(success => success.HasForceMute.Should().BeTrue());

        [Fact]
        public void Build_ShouldEnableVideo_GivenEnableVideoIsUsed() =>
            this.BuildBaseRequest()
                .EnableVideo()
                .Create()
                .Map(request => request.Sip)
                .Should()
                .BeSuccess(success => success.HasVideo.Should().BeTrue());

        [Fact]
        public void Build_ShouldOverrideCustomHeaderKey_GivenWithHeaderKeyIsUsedOnExistingKey() =>
            this.BuildBaseRequest()
                .WithHeader("key1", "value1")
                .WithHeader("key1", "value2")
                .Create()
                .Map(request => request.Sip)
                .Map(element => element.Headers)
                .Should()
                .BeSuccess(success => success.Should().BeSome(some => some["key1"].Should().Be("value2")));

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            InitiateCallRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithToken(this.token)
                .WithSipUri(this.uri)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            InitiateCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithToken(this.token)
                .WithSipUri(this.uri)
                .Create()
                .Should()
                .BeParsingFailure("SessionId cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenTokenIsNullOrWhitespace(string value) =>
            InitiateCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithToken(value)
                .WithSipUri(this.uri)
                .Create()
                .Should()
                .BeParsingFailure("Token cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenMandatoryValuesAreProvided() =>
            this.BuildBaseRequest()
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Token.Should().Be(this.token);
                    request.Sip.Uri.Should().Be(this.uri);
                    request.Sip.Authentication.Should().BeNone();
                    request.Sip.Headers.Should().BeNone();
                    request.Sip.HasVideo.Should().BeFalse();
                    request.Sip.HasEncryptedMedia.Should().BeFalse();
                    request.Sip.HasForceMute.Should().BeFalse();
                });

        [Fact]
        public void Build_ShouldSetAuthentication_GivenWithAuthenticationIsUsed() =>
            this.BuildBaseRequest()
                .WithAuthentication(this.authentication)
                .Create()
                .Map(request => request.Sip)
                .Should()
                .BeSuccess(success => success.Authentication.Should().BeSome(this.authentication));

        [Fact]
        public void Build_ShouldSetCustomHeaderKey_GivenWithHeaderKeyIsUsed() =>
            this.BuildBaseRequest()
                .WithHeader("key1", "value1")
                .Create()
                .Map(request => request.Sip)
                .Map(element => element.Headers)
                .Should()
                .BeSuccess(success => success.Should().BeSome(some => some["key1"].Should().Be("value1")));

        [Fact]
        public void Build_ShouldSetFrom_GivenWithFromIsUsed() =>
            this.BuildBaseRequest()
                .WithFrom(this.from)
                .Create()
                .Map(request => request.Sip)
                .Should()
                .BeSuccess(success => success.From.Should().BeSome(this.from));

        private IBuilderForOptionalSip BuildBaseRequest() =>
            InitiateCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithToken(this.token)
                .WithSipUri(this.uri);
    }
}