using System;
using Vonage.Serialization;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.Common.Extensions;
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
                JsonSerializerBuilder.BuildWithCamelCase());

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