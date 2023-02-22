using System;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast.AddStreamToBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.AddStreamToBroadcast
{
    public class AddStreamToBroadcastSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public AddStreamToBroadcastSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(AddStreamToBroadcastSerializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldSerialize() =>
            AddStreamToBroadcastRequestBuilder.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId("broadcastId")
                .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
                .WithDisabledAudio()
                .WithDisabledVideo()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            AddStreamToBroadcastRequestBuilder.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId("broadcastId")
                .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}