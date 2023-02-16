using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.InitiateCall;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.InitiateCall
{
    public class InitiateCallSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public InitiateCallSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(InitiateCallSerializationTest).Namespace,
                JsonSerializer.BuildWithCamelCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<InitiateCallResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Id.Should().Be("8d78f9ca-c336-497e-9264-05aa2a442dcc");
                    success.ConnectionId.Should().Be(new Guid("5fb383d1-a70f-4153-b009-2ef0429bf08f"));
                    success.StreamId.Should().Be("1b934954-f1b4-406a-8bb8-7cde7a8be2a3");
                });

        [Fact]
        public void ShouldSerialize() =>
            SipElementBuilder
                .Build("sip:user@sip.partner.com;transport=tls")
                .EnableVideo()
                .EnableForceMute()
                .EnableEncryptedMedia()
                .WithFrom("from@example.com")
                .WithHeaderKey("some-value")
                .WithAuthentication(new SipElement.SipAuthentication("username", "p@ssw0rd"))
                .Create()
                .Bind(sip => InitiateCallRequest.Parse(Guid.NewGuid(), "b40ef09b-3811-4726-b508-e41a0f96c68f",
                    "78d335fa-323d-0114-9c3d-d6f0d48968cf", sip))
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            SipElementBuilder
                .Build("sip:user@sip.partner.com;transport=tls")
                .Create()
                .Bind(sip => InitiateCallRequest.Parse(Guid.NewGuid(), "b40ef09b-3811-4726-b508-e41a0f96c68f",
                    "78d335fa-323d-0114-9c3d-d6f0d48968cf", sip))
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}