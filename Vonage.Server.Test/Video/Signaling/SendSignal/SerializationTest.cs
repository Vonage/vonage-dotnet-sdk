using System;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Video.Signaling;
using Vonage.Video.Signaling.SendSignal;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignal
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() => this.helper =
            new SerializationTestHelper(typeof(SerializationTest).Namespace, JsonSerializerBuilder.Build());

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
}