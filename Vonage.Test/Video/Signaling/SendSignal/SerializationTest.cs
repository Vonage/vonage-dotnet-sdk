using System;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Signaling;
using Vonage.Video.Signaling.SendSignal;
using Xunit;

namespace Vonage.Test.Video.Signaling.SendSignal;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper;

    public SerializationTest() => this.helper =
        new SerializationTestHelper(typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldSerialize() =>
        SendSignalRequest.Build()
            .WithApplicationId(Guid.NewGuid())
            .WithSessionId("Some session Id")
            .WithConnectionId("Some connection Id")
            .WithContent(new SignalContent("chat", "Text of the chat message"))
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}