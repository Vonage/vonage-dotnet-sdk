using System;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast.RemoveStreamFromBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.RemoveStreamFromBroadcast
{
    public class RemoveStreamFromBroadcastSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public RemoveStreamFromBroadcastSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(RemoveStreamFromBroadcastSerializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldSerialize() =>
            RemoveStreamFromBroadcastRequestBuilder.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId(Guid.NewGuid())
                .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}