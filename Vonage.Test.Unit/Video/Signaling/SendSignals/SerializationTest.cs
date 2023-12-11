using System;
using System.Text.Json;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Video.Signaling;
using Vonage.Video.Signaling.SendSignals;
using Xunit;

namespace Vonage.Test.Unit.Video.Signaling.SendSignals
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() => this.helper =
            new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.Build(JsonNamingPolicy.CamelCase));

        [Fact]
        public void ShouldSerialize() =>
            SendSignalsRequest.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithSessionId("Some session Id")
                .WithContent(new SignalContent("chat", "Text of the chat message"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}