using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.InitiateCall;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.InitiateCall
{
    public class SipElementBuilderTest
    {
        private readonly SipElement.SipAuthentication authentication;
        private readonly string uri;
        private readonly string from;

        public SipElementBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.uri = fixture.Create<string>();
            this.from = fixture.Create<string>();
            this.authentication = new SipElement.SipAuthentication(fixture.Create<string>(), fixture.Create<string>());
        }

        [Fact]
        public void Build_ShouldEnableEncryptedMedia_GivenEnableEncryptedMediaIsUsed() =>
            SipElementBuilder.Build(this.uri)
                .EnableEncryptedMedia()
                .Create()
                .Should()
                .BeSuccess(success => success.HasEncryptedMedia.Should().BeTrue());

        [Fact]
        public void Build_ShouldEnableForceMute_GivenEnableForceMuteIsUsed() =>
            SipElementBuilder.Build(this.uri)
                .EnableForceMute()
                .Create()
                .Should()
                .BeSuccess(success => success.HasForceMute.Should().BeTrue());

        [Fact]
        public void Build_ShouldEnableVideo_GivenEnableVideoIsUsed() =>
            SipElementBuilder.Build(this.uri)
                .EnableVideo()
                .Create()
                .Should()
                .BeSuccess(success => success.HasVideo.Should().BeTrue());

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            SipElementBuilder.Build(this.uri)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.Uri.Should().Be(this.uri);
                    success.Authentication.Should().BeNone();
                    success.Headers.Should().BeNone();
                    success.HasVideo.Should().BeFalse();
                    success.HasEncryptedMedia.Should().BeFalse();
                    success.HasForceMute.Should().BeFalse();
                });

        [Fact]
        public void Build_ShouldOverrideCustomHeaderKey_GivenWithHeaderKeyIsUsedOnExistingKey() =>
            SipElementBuilder.Build(this.uri)
                .WithHeader("key1", "value1")
                .WithHeader("key1", "value2")
                .Create()
                .Map(element => element.Headers)
                .Should()
                .BeSuccess(success => success.Should().BeSome(some => some["key1"].Should().Be("value2")));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenUriIsNullOrWhitespace(string invalidUri) =>
            SipElementBuilder
                .Build(invalidUri)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Uri cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldSetAuthentication_GivenWithAuthenticationIsUsed() =>
            SipElementBuilder.Build(this.uri)
                .WithAuthentication(this.authentication)
                .Create()
                .Should()
                .BeSuccess(success => success.Authentication.Should().BeSome(this.authentication));

        [Fact]
        public void Build_ShouldSetCustomHeaderKey_GivenWithHeaderKeyIsUsed() =>
            SipElementBuilder.Build(this.uri)
                .WithHeader("key1", "value1")
                .Create()
                .Map(element => element.Headers)
                .Should()
                .BeSuccess(success => success.Should().BeSome(some => some["key1"].Should().Be("value1")));

        [Fact]
        public void Build_ShouldSetFrom_GivenWithFromIsUsed() =>
            SipElementBuilder.Build(this.uri)
                .WithFrom(this.from)
                .Create()
                .Should()
                .BeSuccess(success => success.From.Should().BeSome(this.from));
    }
}