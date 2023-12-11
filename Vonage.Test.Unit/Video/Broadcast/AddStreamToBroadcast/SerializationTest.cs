using System;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Video.Broadcast.AddStreamToBroadcast;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.AddStreamToBroadcast
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId(Guid.NewGuid())
                .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
                .WithDisabledAudio()
                .WithDisabledVideo()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId(Guid.NewGuid())
                .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}