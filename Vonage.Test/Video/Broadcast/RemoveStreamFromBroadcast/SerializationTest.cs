﻿using System;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.RemoveStreamFromBroadcast;
using Xunit;

namespace Vonage.Test.Video.Broadcast.RemoveStreamFromBroadcast
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            RemoveStreamFromBroadcastRequest.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId(Guid.NewGuid())
                .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}