#region
using System;
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sip.InitiateCall;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sip.InitiateCall;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<InitiateCallResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyCall);

    [Fact]
    public void ShouldSerialize() =>
        InitiateCallRequest.Build()
            .WithApplicationId(Guid.NewGuid())
            .WithSessionId("b40ef09b-3811-4726-b508-e41a0f96c68f")
            .WithToken("123")
            .WithSipUri(new Uri("sip:user@sip.partner.com;transport=tls"))
            .EnableVideo()
            .EnableForceMute()
            .EnableEncryptedMedia()
            .WithAuthentication(new SipElement.SipAuthentication("username", "p@ssw0rd"))
            .WithFrom("from@example.com")
            .WithHeader("headerKey", "some-value")
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithDefaultValues() =>
        InitiateCallRequest.Build()
            .WithApplicationId(Guid.NewGuid())
            .WithSessionId("b40ef09b-3811-4726-b508-e41a0f96c68f")
            .WithToken("123")
            .WithSipUri(new Uri("sip:user@sip.partner.com;transport=tls"))
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    internal static void VerifyCall(InitiateCallResponse success)
    {
        success.Id.Should().Be("8d78f9ca-c336-497e-9264-05aa2a442dcc");
        success.ConnectionId.Should().Be(new Guid("5fb383d1-a70f-4153-b009-2ef0429bf08f"));
        success.StreamId.Should().Be("1b934954-f1b4-406a-8bb8-7cde7a8be2a3");
    }
}